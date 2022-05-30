namespace Domain.Models
{
    using Domain.Models.Core;
    using Domain.Models.Validations;

    public class Line : Entity
    {
        public Line(Item item, int quantity)
        {
            Id = Guid.NewGuid();
            Item = item;
            Quantity = quantity;
        }

        #region public fields
        public Item Item { get; private set; }

        public int Quantity { get; private set; }

        public virtual decimal DiscountAmount { get; private set; }

        public decimal Subtotal => (Item.Price * Quantity) - DiscountAmount;
        
        #endregion

        #region public methods
        public void IncrementQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public void AddDiscount(decimal discountValue)
        {
            DiscountAmount += discountValue;
        }

        public override bool IsValid() => new LineValidation().Validate(this).IsValid;

        public static Line CreateLine(Item item, int quantity) => new(item, quantity);

        #endregion
    }
}