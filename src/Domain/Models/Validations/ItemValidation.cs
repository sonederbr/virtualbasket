namespace Domain.Models.Validations
{
    using FluentValidation;

    public class ItemValidation : AbstractValidator<Item>
    {
        public ItemValidation()
        {
            RuleFor(c => c.Price)
                .GreaterThan(0)
                .WithMessage("Value should be greater than 0!");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name should not be empty!");
        }
    }
}