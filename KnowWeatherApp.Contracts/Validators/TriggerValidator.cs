using FluentValidation;
using KnowWeatherApp.Contracts.Models.Triggers;
using System;

namespace KnowWeatherApp.Contracts.Validators
{
    public class TriggerValidator : AbstractValidator<TriggerEditDto>
    {
        public TriggerValidator()
        {
            RuleFor(x => x.Threshold).NotEmpty().Must(ShouldBeNumber).WithMessage("Threshold should be a valid number");
        }

        private bool ShouldBeNumber(string arg)
        {
            return Double.TryParse(arg, out double _);
        }
    }
}
