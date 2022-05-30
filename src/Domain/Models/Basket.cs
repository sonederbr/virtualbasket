namespace Domain.Models
{
    using Domain.Models.Core;
    using Domain.Models.Validations;
    using Domain.Services.DiscountRules;

    public class Basket : Entity, IAgreegateRoot
    {
        private List<Line> _lines;

        private Basket(Guid customerId)
        {
            CustomerId = customerId;
            Total = 0;
            _lines = new List<Line>();
        }

        #region public fields
        public Guid CustomerId { get; private set; }

        public decimal Total { get; private set; }

        public decimal Discount { get; private set; }

        public IReadOnlyCollection<Line> Lines { get => _lines; }
        #endregion

        #region public methods
        public Line AddItem(Item item, int quantity)
        {
            Line line;
            if (IsItemAlreadyInBasket(item))
            {
                line = _lines.Single(p => p.Item.Name == item.Name);
                line.IncrementQuantity(quantity);
            }
            else
            {
                line = new Line(item, quantity);
                _lines.Add(line);
            }

            CalculateTotalValue();

            return line;
        }

        public void RemoveItem(Item item)
        {
            if (IsItemAlreadyInBasket(item))
                _lines.Remove(_lines.Single(p => p.Item.Name == item.Name));
        }

        public void AddDiscount(Line line, decimal discountValue)
        {
            if (IsItemAlreadyInBasket(line.Item))
            {
                _lines.Single(p => p.Item.Name == line.Item.Name)
                    .AddDiscount(discountValue);
            }

            CalculateTotalValue();
        }

        public void CalculateTotalValue()
        {
            Total = Lines.Sum(p => p.Subtotal);
            Discount = Lines.Sum(p => p.DiscountAmount);
        }

        public override bool IsValid()
        {
            return new BasketValidation().Validate(this).IsValid &&
                _lines.Any(p => !p.IsValid()) &&
                _lines.Any(p => !p.Item.IsValid());
        }

        public static Basket CreateBasket(Guid customerId) => new(customerId);
        #endregion

        #region private methods
        private bool IsItemAlreadyInBasket(Item item) => _lines.Any(p => p.Item.Name == item.Name);

        //private bool IsNotDiscountAlreadyAdded(Item item, Discount discount) =>
        //    _lines.Any(p => p.Item.Name == item.Name && !p.Discounts.Any(p => p.GetType() == discount.GetType()));
        #endregion
    }
}