namespace Application.DTOs.MealEntry
{
    public class MealEntryFilter
    {
        public int? CurrentUserId { get; set; }
        public string? Name { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
