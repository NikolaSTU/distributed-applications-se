namespace Application.DTOs.FoodEntry
{
    public class FoodEntryFilter
    {
        public int? CurrentUserId { get; set; }
        public int? FoodId { get; set; }
        public int? MealEntryId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
