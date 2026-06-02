using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.GlucoseEntry
{
    public class GlucoseEntryResponseDTO : IResponseDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public decimal Value { get; set; }
        public string Source { get; set; } = string.Empty;
        public DateTime MeasuredAt { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
