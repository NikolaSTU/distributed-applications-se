using Application.DTOs.FoodEntry;
using Application.DTOs.MealEntry;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMealEntryService : IBaseService<MealEntry, MealEntryResponseDTO, MealEntryCreateUpdateDTO, MealEntryFilter>
    {
        Task<decimal> CalculateDose(int id, int userId);

    }
}
