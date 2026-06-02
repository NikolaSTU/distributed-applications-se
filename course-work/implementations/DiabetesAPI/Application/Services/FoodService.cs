using Application.DTOs.Food;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FoodService : BaseService<Food, FoodResponseDTO, FoodCreateUpdateDTO, FoodFilter>, IFoodService
    {

        public FoodService(IGenericRepository<Food> foodRepository, IMapper mapper)
            : base(foodRepository, mapper)
        {
        }

        public override IQueryable<Food> ApplyFiltering(IQueryable<Food> query, FoodFilter filters)
        {
            if (filters.CurrentUserId.HasValue)
            {
                query = query.Where(e => e.UserId == filters.CurrentUserId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filters.Name))
            {
                query = query.Where(e => e.Name.Contains(filters.Name));
            }

            if (filters.MinGlycemicIndex.HasValue)
            {
                query = query.Where(e => e.GlycemicIndex >= filters.MinGlycemicIndex.Value);
            }

            if (filters.MaxGlycemicIndex.HasValue)
            {
                query = query.Where(e => e.GlycemicIndex <= filters.MaxGlycemicIndex.Value);
            }

            if (filters.MinCalories.HasValue)
            {
                query = query.Where(e => e.CaloriesPer100g >= filters.MinCalories.Value);
            }

            if (filters.MaxCalories.HasValue)
            {
                query = query.Where(e => e.CaloriesPer100g <= filters.MaxCalories.Value);
            }

            if (filters.MinCarbs.HasValue)
            {
                query = query.Where(e => e.CarbPer100g >= filters.MinCarbs.Value);
            }

            if (filters.MaxCarbs.HasValue)
            {
                query = query.Where(e => e.CarbPer100g <= filters.MaxCarbs.Value);
            }

            return query;
        }

        public override async Task<FoodResponseDTO> CreateAsync(FoodCreateUpdateDTO dto)
        {
            var food = _mapper.Map<Food>(dto);
            food.CaloriesPer100g = (dto.CarbPer100g * 4) + (dto.ProteinPer100g * 4) + (dto.FatPer100g * 9);

            await _repository.AddAsync(food);
            await _repository.SaveChangesAsync();

            return _mapper.Map<FoodResponseDTO>(food);

        }

        public override async Task<bool> UpdateAsync(int id, FoodCreateUpdateDTO dto)
        {
            var food = await _repository.GetByIdAsync(id);
            if (food == null) return false;

            _mapper.Map(dto, food);
            food.CaloriesPer100g = (dto.CarbPer100g * 4) + (dto.ProteinPer100g * 4) + (dto.FatPer100g * 9);


            _repository.Update(food);
            return await _repository.SaveChangesAsync();
        }
    }
}
