using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.GlucoseEntry
{
    public class GlucoseEntryFilter
    {
 
        public string? Note { get; set; }
        public string? Source { get; set; }
        public decimal? MaxValue { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? CurrentUserId { get; set; }
    }
}
