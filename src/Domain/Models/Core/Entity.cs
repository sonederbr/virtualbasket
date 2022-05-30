namespace Domain.Models.Core
{
    using FluentValidation.Results;

    public class Entity
    {
        public Guid Id { get; protected set; }

        public ValidationResult ValidationResult { get; set; }

        public virtual bool IsValid() => false;
    }
}