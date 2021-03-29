using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.CarName).NotNull();
            RuleFor(c => c.CarName).MinimumLength(2);

            RuleFor(c => c.Description).NotNull();
            RuleFor(c => c.Description).MinimumLength(2);

            RuleFor(c => c.FuelType).NotNull();
            RuleFor(c => c.FuelType).MinimumLength(2);

            RuleFor(c => c.Description).MinimumLength(5);
        }
    }
}