using Domain.Interfaces;

namespace Domain.Entities
{
    public class Food : IEntity
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal CarbPer100g { get; set; }
        public decimal ProteinPer100g { get; set; }
        public decimal FatPer100g { get; set; }
        public decimal GlycemicIndex {  get; set; }
        public decimal CaloriesPer100g { get; set; }


    }
}