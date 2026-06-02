using Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class FoodEntry : IEntity
    {
        public int Id { get; set; }
        public int? UserId { get; set; }

        public int FoodId { get; set; }
        [ForeignKey("FoodId")]
        public Food Food { get; set; }
        public int MealEntryId { get; set; }
        [ForeignKey("MealEntryId")]
        public MealEntry MealEntry { get; set; }

        public decimal Weigth { get; set; }
        public decimal Carbs { get; set; }
        public decimal Protein { get; set; }
        public decimal Fat { get; set; }
        public decimal Calories { get; set; }




    }
}