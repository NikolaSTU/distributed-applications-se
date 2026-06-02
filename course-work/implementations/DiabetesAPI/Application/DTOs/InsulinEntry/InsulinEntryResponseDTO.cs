using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.InsulinEntry
{
    public class InsulinEntryResponseDTO : IResponseDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public decimal Units { get; set; }
        public string Type { get; set; }
        public DateTime InjectedAt { get; set; }
        public string Note { get; set; }
    }
}
