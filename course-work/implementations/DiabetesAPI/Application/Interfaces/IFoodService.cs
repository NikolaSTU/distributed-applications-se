using Application.DTOs.Food;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFoodService : IBaseService<Food, FoodResponseDTO, FoodCreateUpdateDTO, FoodFilter>
    { 
    }
}
