using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.FoodEntry
{
    public class FoodEntryCreateUpdateDTOValidator : AbstractValidator<FoodEntryCreateUpdateDTO>
    {
        public FoodEntryCreateUpdateDTOValidator()
        {
            RuleFor(x => x.FoodId)
                .NotEmpty().WithMessage("Идентификаторът на храната е задължителен.")
                .GreaterThan(0).WithMessage("Невалиден идентификатор на храна.");

            RuleFor(x => x.MealEntryId)
                .NotEmpty().WithMessage("Идентификаторът на храненето е задължителен.")
                .GreaterThan(0).WithMessage("Невалиден идентификатор на хранене.");

            RuleFor(x => x.Weigth)
                .NotEmpty().WithMessage("Грамажът е задължителен.")
                .GreaterThan(0m).WithMessage("Грамажът трябва да бъде по-голям от 0 грама.")
                .LessThanOrEqualTo(5000m).WithMessage("Въведеният грамаж е прекалено голям за една порция (максимум 5000г).");
        }
    }
}
