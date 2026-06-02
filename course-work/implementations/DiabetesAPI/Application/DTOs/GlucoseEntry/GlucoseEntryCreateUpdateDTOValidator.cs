using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.GlucoseEntry
{
    public class GlucoseEntryCreateUpdateDTOValidator : AbstractValidator<GlucoseEntryCreateUpdateDTO>
    {
        public GlucoseEntryCreateUpdateDTOValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("Стойността на глюкозата е задължителна.")
                .InclusiveBetween(0.1m, 35.0m).WithMessage("Стойността на кръвната захар трябва да бъде между 0.1 и 35.0 mmol/L.");

            RuleFor(x => x.Source)
                .NotEmpty().WithMessage("Източникът на измерване е задължителен.")
                .MaximumLength(50).WithMessage("Източникът не може да бъде по-дълъг от 50 символа.");

            RuleFor(x => x.MeasuredAt)
                .NotEmpty().WithMessage("Датата и часът на измерване са задължителни.")
                .LessThanOrEqualTo(x => DateTime.UtcNow.AddMinutes(5))
                .WithMessage("Не можете да въвеждате измерване с бъдеща дата и час.");

            RuleFor(x => x.Note)
                .MaximumLength(500).WithMessage("Бележката не може да бъде по-дълга от 500 символа.");
        }
    }
}
