using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.FoodEntry
{
    public class FoodEntryResponseDTO : IResponseDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int FoodId { get; set; }
        public int MealEntryId { get; set; }
        public decimal Weigth { get; set; }
        public decimal Carbs { get; set; }
        public decimal Protein { get; set; }
        public decimal Fat { get; set; }
        public decimal Calories { get; set; }
    }
}
