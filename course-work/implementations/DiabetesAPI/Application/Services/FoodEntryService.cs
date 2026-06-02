using Application.DTOs.Food;
using Application.DTOs.FoodEntry;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FoodEntryService : BaseService<FoodEntry, FoodEntryResponseDTO, FoodEntryCreateUpdateDTO, FoodEntryFilter>, IFoodEntryService
    {
        private readonly IGenericRepository<Food> _foodRepository;

        public FoodEntryService(IGenericRepository<FoodEntry> foodEntryRepository,
            IGenericRepository<Food> foodRepository, IMapper mapper)
            : base(foodEntryRepository, mapper)
        {
            _foodRepository = foodRepository;
        }

        public override IQueryable<FoodEntry> ApplyFiltering(IQueryable<FoodEntry> query, FoodEntryFilter filters)
        {
            if (filters.CurrentUserId.HasValue)
            {
                query = query.Where(e => e.UserId == filters.CurrentUserId.Value);
            }

            if (filters.FoodId.HasValue)
            {
                query = query.Where(e => e.FoodId == filters.FoodId.Value);
            }

            if (filters.MealEntryId.HasValue)
            {
                query = query.Where(e => e.MealEntryId == filters.MealEntryId.Value);
            }

            if (filters.FromDate.HasValue)
            {
                query = query.Where(e => e.MealEntry.CreatedAt >= filters.FromDate.Value);
            }

            if (filters.ToDate.HasValue)
            {
                query = query.Where(e => e.MealEntry.CreatedAt <= filters.ToDate.Value);
            }

            return query;
        }

        public override async Task<FoodEntryResponseDTO> CreateAsync(FoodEntryCreateUpdateDTO dto)
        {
            var foodEntry = _mapper.Map<FoodEntry>(dto);

            var food = await _foodRepository.GetByIdAsync(foodEntry.FoodId);

            if (food == null)
            {
                throw new KeyNotFoundException($"Храна с ID {foodEntry.FoodId} не беше намерена в базата данни.");
            }

            foodEntry.Calories = (foodEntry.Weigth * food.CaloriesPer100g) / 100;
            foodEntry.Carbs = (foodEntry.Weigth * food.CarbPer100g) / 100;
            foodEntry.Protein = (foodEntry.Weigth * food.ProteinPer100g) / 100;
            foodEntry.Fat = (foodEntry.Weigth * food.FatPer100g) / 100;

            await _repository.AddAsync(foodEntry);
            await _repository.SaveChangesAsync();

            return _mapper.Map<FoodEntryResponseDTO>(foodEntry);
        }
        
    }
}
