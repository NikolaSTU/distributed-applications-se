namespace Application.DTOs.Food
{
    public class FoodFilter
    {
        public int? CurrentUserId { get; set; }
        public string? Name { get; set; }
        public decimal? MinGlycemicIndex { get; set; }
        public decimal? MaxGlycemicIndex { get; set; }
        public decimal? MinCalories { get; set; }
        public decimal? MaxCalories { get; set; }
        public decimal? MinCarbs { get; set; }
        public decimal? MaxCarbs { get; set; }
    }
}
