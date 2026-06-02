using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.FoodEntry
{
    public class FoodEntryCreateUpdateDTO : ICreateUpdateDTO
    {
        [JsonIgnore]
        public int? UserId { get; set; }
        public int FoodId { get; set; }
        public int MealEntryId { get; set; }
        public decimal Weigth { get; set; }
    }
}
