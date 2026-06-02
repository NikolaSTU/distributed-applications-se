using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.MealEntry
{
    public class MealEntryCreateUpdateDTO : ICreateUpdateDTO
    {
        [JsonIgnore]
        public int? UserId { get; set; }
        public string Name { get; set; }
    }
}
