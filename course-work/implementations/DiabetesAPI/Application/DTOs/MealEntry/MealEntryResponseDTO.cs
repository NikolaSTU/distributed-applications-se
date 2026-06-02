using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MealEntry
{
    public class MealEntryResponseDTO : IResponseDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public decimal TotalCarb { get; set; }
        public decimal TotalProtein { get; set; }
        public decimal TotalFat { get; set; }
        public decimal TotalCalories { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
