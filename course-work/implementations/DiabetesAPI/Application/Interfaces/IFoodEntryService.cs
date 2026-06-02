using Application.DTOs.Food;
using Application.DTOs.FoodEntry;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFoodEntryService : IBaseService<FoodEntry, FoodEntryResponseDTO, FoodEntryCreateUpdateDTO, FoodEntryFilter>
    {
    }
}
