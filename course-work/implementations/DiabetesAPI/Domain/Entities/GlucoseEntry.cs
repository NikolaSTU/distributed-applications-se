using Domain.Interfaces;

namespace Domain.Entities
{
    public class GlucoseEntry : IEntity
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public decimal Value { get; set; }
        public string Source { get; set; } = string.Empty;
        public DateTime MeasuredAt { get; set; } = DateTime.UtcNow;
        public string Note { get; set; } = string.Empty;

        public User User { get; set; } = null;
    }
}