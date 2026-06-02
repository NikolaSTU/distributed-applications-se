using Domain.Interfaces;

namespace Domain.Entities
{
    public class MealEntry : IEntity
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
        public ICollection<FoodEntry> FoodEntries { get; set; } = new List<FoodEntry>();
    }
}