namespace Domain.Models.Validations
{
    using FluentValidation;

    public class BasketValidation : AbstractValidator<Basket>
    {
        public BasketValidation()
        {
            RuleFor(c => c.Lines)
                .NotNull()
                .WithMessage("Lines should not be null!");

            RuleFor(c => c.Lines)
                .Must(c => c == null || c.Any())
                .WithMessage("Lines should not be empty!");

            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customer Id is inválid!");
        }
    }
}