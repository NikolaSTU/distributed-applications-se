using Domain.Interfaces;
using System.Text.Json.Serialization;

namespace Application.DTOs.Food
{
    public class FoodCreateUpdateDTO : ICreateUpdateDTO
    {
        [JsonIgnore]
        public int? UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal CarbPer100g { get; set; }
        public decimal ProteinPer100g { get; set; }
        public decimal FatPer100g { get; set; }
        public decimal GlycemicIndex { get; set; }
    }
}
