using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UserUpdateDTOValidator : AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateDTOValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Потребителското име е задължително.")
                .MinimumLength(3).WithMessage("Потребителското име трябва да съдържа поне 3 символа.")
                .MaximumLength(50).WithMessage("Потребителското име не може да бъде по-дълго от 50 символа.")
                .Matches(@"^[a-zA-Z0-9_\.]+$").WithMessage("Потребителското име може да съдържа само букви, цифри, долна черта (_) и точка (.)");

            RuleFor(x => x.ICR)
                .NotEmpty().WithMessage("Въглехидратният фактор (ICR) е задължителен.")
                .InclusiveBetween(1m, 50m).WithMessage("Въглехидратният фактор (ICR) трябва да бъде между 1 и 50 грама/единица.");

            RuleFor(x => x.ISF)
                .NotEmpty().WithMessage("Факторът на чувствителност (ISF) е задължителен.")
                .InclusiveBetween(0.5m, 15.0m).WithMessage("Факторът на чувствителност (ISF) трябва да бъде между 0.5 и 15.0 mmol/L.");

            RuleFor(x => x.TargetGlucose)
                .NotEmpty().WithMessage("Целевата глюкоза е задължителна.")
                .InclusiveBetween(4.0m, 10.0m).WithMessage("Целевата глюкоза трябва да бъде в здравословни граници между 4.0 и 10.0 mmol/L.");
        }
    }
}
