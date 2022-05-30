namespace Domain.Models
{
    using Domain.Models.Core;
    using Domain.Models.Validations;

    public class Item : Entity
    {
        public Item(string name, decimal price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
        }

        #region public fields
        public string Name { get; private set; }

        public decimal Price { get; private set; }
        #endregion

        #region public methods
        public static Item CreateItem(string name, decimal value) =>
            new(name, value);

        public override bool IsValid() => new ItemValidation().Validate(this).IsValid;
        #endregion
    }
}