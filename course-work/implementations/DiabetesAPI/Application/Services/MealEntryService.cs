using Application.DTOs.FoodEntry;
using Application.DTOs.MealEntry;
using Application.DTOs.Paging;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MealEntryService : BaseService<MealEntry, MealEntryResponseDTO, MealEntryCreateUpdateDTO, MealEntryFilter>, IMealEntryService
    {
        private readonly IGenericRepository<FoodEntry> _foodEntryRepository;
        private readonly IGenericRepository<GlucoseEntry> _glucoseEntryRepository;
        private readonly IGenericRepository<InsulinEntry> _insulinEntryRepository;
        private readonly IGenericRepository<User> _userRepository;

        public MealEntryService(
            IGenericRepository<MealEntry> mealEntryRepository,
            IGenericRepository<FoodEntry> foodEntryRepository,
            IGenericRepository<GlucoseEntry> glucoseEntryRepository,
            IGenericRepository<User> userRepository,
            IGenericRepository<InsulinEntry> insulinEntryRepository,
            IMapper mapper)
            : base(mealEntryRepository, mapper)
        {
            _foodEntryRepository = foodEntryRepository;
            _glucoseEntryRepository = glucoseEntryRepository;
            _insulinEntryRepository = insulinEntryRepository;
            _userRepository = userRepository;
        }

        public override IQueryable<MealEntry> ApplyFiltering(IQueryable<MealEntry> query, MealEntryFilter filters)
        {
            if (filters.CurrentUserId.HasValue)
            {
                query = query.Where(e => e.UserId == filters.CurrentUserId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filters.Name))
            {
                query = query.Where(e => e.Name.Contains(filters.Name));
            }

            if (filters.FromDate.HasValue)
            {
                query = query.Where(e => e.CreatedAt >= filters.FromDate.Value);
            }

            if (filters.ToDate.HasValue)
            {
                query = query.Where(e => e.CreatedAt <= filters.ToDate.Value);
            }

            return query;
        }

        public override async Task<MealEntryResponseDTO> GetByIdAsync(int id)
        {
            var mealEntry = await _repository.GetByIdAsync(id);
            if (mealEntry == null) return null;

            var response = _mapper.Map<MealEntryResponseDTO>(mealEntry);

            
            var foodEntries = await _foodEntryRepository.FindAsync(fe => fe.MealEntryId == id);
            if (foodEntries != null)
            {
                foreach (var item in foodEntries)
                {
                    response.TotalCarb += item.Carbs;
                    response.TotalProtein += item.Protein;
                    response.TotalFat += item.Fat;
                    response.TotalCalories += item.Calories;
                }
            }

            return response;
        }

        public override async Task<PagedResult<MealEntryResponseDTO>> GetPagedAsync(
        BaseQueryParameters<MealEntryFilter> query)
        {
            var result = await base.GetPagedAsync(query);

            var allFoodEntries = await _foodEntryRepository.GetAllAsync();

            var groupedFoodEntries = allFoodEntries
                .GroupBy(fe => fe.MealEntryId)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var meal in result.Items)
            {
                if (groupedFoodEntries.TryGetValue(meal.Id, out var foodEntries))
                {
                    meal.TotalCarb = foodEntries.Sum(x => x.Carbs);
                    meal.TotalProtein = foodEntries.Sum(x => x.Protein);
                    meal.TotalFat = foodEntries.Sum(x => x.Fat);
                    meal.TotalCalories = foodEntries.Sum(x => x.Calories);
                }
            }

            return result;
        }

        public async Task<decimal> CalculateDose(int id, int userId)
        {
            var mealEntry = await GetByIdAsync(id);
            var user = await _userRepository.GetByIdAsync(userId);

            var latestGlucoseEntry = await _glucoseEntryRepository.FindLatestAsync(
                x => x.UserId == userId,
                x => x.MeasuredAt
            );

            if (latestGlucoseEntry == null)
                throw new Exception("No glucose entry found for user");

            var latestInsulinEntry = await _insulinEntryRepository.FindLatestAsync(
                x => x.UserId == userId,
                x => x.InjectedAt
            );

            decimal iob = 0;

            if (latestInsulinEntry != null)
            {
                var now = DateTime.UtcNow;

                double minutesSinceInjection =
                    (now - latestInsulinEntry.InjectedAt).TotalMinutes;

                if (minutesSinceInjection > 0)
                {
                    iob = CalculateIOB(
                        latestInsulinEntry.Units,
                        minutesSinceInjection
                    );
                }
            }

            decimal carbs = mealEntry.TotalCarb;
            decimal currentGlucose = latestGlucoseEntry.Value;
            decimal targetGlucose = user.TargetGlucose;
            decimal icr = user.ICR;
            decimal isf = user.ISF;

            decimal carbBolus = carbs / icr;

            decimal correction = (currentGlucose - targetGlucose) / isf;

            if (correction < 0)
                correction = 0;

            decimal dose = carbBolus + correction - iob;

            if (dose < 0) dose = 0;
            if (dose > 25) dose = 25;

            return Math.Round(dose, 2);
        }


        private decimal CalculateIOB(decimal units, double minutes)
        {
            const double DIA = 240.0;
            const double onset = 10.0;

            if (minutes <= onset)
                return 0;

            if (minutes >= DIA)
                return 0;

            double t = (minutes - onset) / (DIA - onset);

            // smooth bell-shaped activity curve
            double activity = 4 * t * (1 - t);

            // normalize safety (keeps within 0..1 range)
            activity = Math.Max(0, Math.Min(1, activity));

            return units * (decimal)activity;
        }
    }
}
