using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MealEntry
{
    public class MealEntryCreateUpdateDTOValidator : AbstractValidator<MealEntryCreateUpdateDTO>
    {
        public MealEntryCreateUpdateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Името на храненето е задължително.")
                .MinimumLength(3).WithMessage("Името на храненето трябва да съдържа поне 3 символа.")
                .MaximumLength(100).WithMessage("Името на храненето не може да бъде по-дълго от 100 символа.");

        }
    }
}
