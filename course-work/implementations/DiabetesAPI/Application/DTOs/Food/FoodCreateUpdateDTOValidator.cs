using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Food
{
    public class FoodCreateUpdateDTOValidator : AbstractValidator<FoodCreateUpdateDTO>
    {
        public FoodCreateUpdateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Името на храната е задължително.")
                .MinimumLength(2).WithMessage("Името трябва да съдържа поне 2 символа.")
                .MaximumLength(100).WithMessage("Името не може да бъде по-дълго от 100 символа.");

            RuleFor(x => x.CarbPer100g)
                .InclusiveBetween(0m, 100m).WithMessage("Въглехидратите трябва да бъдат между 0 и 100 грама.");

            RuleFor(x => x.ProteinPer100g)
                .InclusiveBetween(0m, 100m).WithMessage("Протеините трябва да бъдат между 0 и 100 грама.");

            RuleFor(x => x.FatPer100g)
                .InclusiveBetween(0m, 100m).WithMessage("Мазнините трябва да бъдат между 0 и 100 грама.");

            RuleFor(x => x)
                .Must(x => (x.CarbPer100g + x.ProteinPer100g + x.FatPer100g) <= 100m)
                .WithMessage("Общият сбор на въглехидрати, протеини и мазнини не може да надвишава 100 грама.");

            RuleFor(x => x.GlycemicIndex)
                .InclusiveBetween(0m, 100m).WithMessage("Гликемичният индекс трябва да бъде между 0 и 100.");
        }
    }
}
