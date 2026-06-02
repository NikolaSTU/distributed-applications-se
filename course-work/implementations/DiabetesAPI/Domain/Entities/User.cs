using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Domain.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public int? UserId { get; set; }

        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public string Role {  get; set; } = "User";
        public decimal ICR { get; set; } = 10; // Insulin-To-Carb Ratio (1:10)
        public decimal ISF { get; set; } = 6; // Insulin Sensitivity Factor (mmol/L)
        public decimal TargetGlucose { get; set; } = 6; // (mmol/L)

        public ICollection<MealEntry> Meals { get; set; } = new List<MealEntry>();
        public ICollection<GlucoseEntry> GlucoseEntries { get; set; } = new List<GlucoseEntry>();
        public ICollection<InsulinEntry> InsulinEntries { get; set; } = new List<InsulinEntry>();

    }
}
