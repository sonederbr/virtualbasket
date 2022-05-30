namespace Domain.Models.Validations
{
    using FluentValidation;

    public class LineValidation : AbstractValidator<Line>
    {
        public LineValidation()
        {
            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity should be greater than 0!");

            RuleFor(c => c.Item)
                .NotNull()
                .WithMessage("Item should not be null!");
        }
    }
}