using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UserResponseDTO : IResponseDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public decimal ICR { get; set; }
        public decimal ISF { get; set; }
        public decimal TargetGlucose { get; set; } = 6;
    }
}
