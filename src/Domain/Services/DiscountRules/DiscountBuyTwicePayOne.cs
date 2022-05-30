namespace Domain.Services.DiscountRules
{
    using Domain.Models;

    public class DiscountBuyTwicePayOne : Discount
    {
        public override decimal DiscountValue()
        {
            if (_line.Quantity == 1)
                return 0;

            if(_line.Quantity % 2 == 0)
            {
                return (_line.Subtotal / 2);
            }

            var quantityRounded = _line.Quantity - 1;
            var quantity = (quantityRounded / 2) + 1;
            var discount = ((quantity * _line.Item.Price) - _line.Subtotal);
            discount = discount < 0 ? discount * -1 : discount;

            return discount;
        }

        public override void Addline(Line line) 
        { 
            _line = line; 
        }
    }
}

