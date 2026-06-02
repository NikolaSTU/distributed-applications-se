using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UserUpdateDTO : ICreateUpdateDTO
    {
        public string Username { get; set; } = string.Empty;
        [JsonIgnore]
        public int? UserId { get; set; }
        public decimal ICR { get; set; } 
        public decimal ISF { get; set; }
        public decimal TargetGlucose { get; set; } = 6;
    }
}
