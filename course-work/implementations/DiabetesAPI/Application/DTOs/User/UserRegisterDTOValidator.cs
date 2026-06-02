using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class UserRegisterDTOValidator : AbstractValidator<UserRegisterDTO>
    {
        public UserRegisterDTOValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Потребителското име е задължително.")
                .MinimumLength(3).WithMessage("Потребителското име трябва да съдържа поне 3 символа.")
                .MaximumLength(50).WithMessage("Потребителското име не може да бъде по-дълго от 50 символа.")
                .Matches(@"^[a-zA-Z0-9_\.]+$").WithMessage("Потребителското име може да съдържа само букви, цифри, долна черта (_) и точка (.)");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Паролата е задължителна.")
                .MinimumLength(6).WithMessage("Паролата трябва да бъде поне 6 символа.")
                .MaximumLength(100).WithMessage("Паролата е прекалено дълга.")
                .Matches(@"[A-Z]").WithMessage("Паролата трябва да съдържа поне една главна буква.")
                .Matches(@"[a-z]").WithMessage("Паролата трябва да съдържа поне една малка буква.")
                .Matches(@"[0-9]").WithMessage("Паролата трябва да съдържа поне една цифра.");
        }
    }
}
