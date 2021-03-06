using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BrandValidator : AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(b => b.BrandName).NotNull();
            RuleFor(b => b.BrandName).MinimumLength(2);

            RuleFor(b => b.InTheGarage).NotNull();
            RuleFor(b => b.InTheGarage).MinimumLength(2);
        }
    }
}