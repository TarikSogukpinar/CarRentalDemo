using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ColorValidator : AbstractValidator<Color>
    {
        public ColorValidator()
        {
            RuleFor(c => c.ColorName).NotNull();
            RuleFor(c => c.ColorName).MinimumLength(2);

            RuleFor(c => c.ColorType).NotNull();
            RuleFor(c => c.ColorType).MinimumLength(2);
        }
    }
}