using Domain.Interfaces;

namespace Domain.Entities
{
    public class InsulinEntry : IEntity
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public decimal Units { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime InjectedAt { get; set; } = DateTime.UtcNow;
        public string Note { get; set; } = string.Empty;

        public User User { get; set; }
    }
}