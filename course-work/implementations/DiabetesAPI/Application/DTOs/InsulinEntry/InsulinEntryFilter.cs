namespace Application.DTOs.InsulinEntry
{
    public class InsulinEntryFilter
    {
        public int? CurrentUserId { get; set; }
        public string? Type { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public decimal? MinUnits { get; set; }
        public decimal? MaxUnits { get; set; }
    }
}
