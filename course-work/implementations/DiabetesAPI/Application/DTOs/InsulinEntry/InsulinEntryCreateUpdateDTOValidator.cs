using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.InsulinEntry
{
    public class InsulinEntryCreateUpdateDTOValidator : AbstractValidator<InsulinEntryCreateUpdateDTO>
    {
        public InsulinEntryCreateUpdateDTOValidator()
        {
            RuleFor(x => x.Units)
                .NotEmpty().WithMessage("Количеството инсулин е задължително.")
                .GreaterThan(0.1m).WithMessage("Инсулиновите единици трябва да бъдат по-големи от 0.1.")
                .LessThanOrEqualTo(100m).WithMessage("Въведеното количество единици е прекалено високо за една доза (максимум 100 единици).");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Типът на инсулина е задължителен.")
                .MaximumLength(50).WithMessage("Типът на инсулина не може да надвишава 50 символа.");

            RuleFor(x => x.InjectedAt)
                .NotEmpty().WithMessage("Датата и часът на инжектиране са задължителни.")
                .LessThanOrEqualTo(x => DateTime.UtcNow.AddMinutes(5))
                .WithMessage("Не можете да въвеждате инжекция с бъдеща дата и час.");

            RuleFor(x => x.Note)
                .MaximumLength(500).WithMessage("Бележката не може да бъде по-дълга от 500 символа.");
        }
    }
}
