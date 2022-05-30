namespace Domain.Services.DiscountRules
{
    using Domain.Models;

    public class DiscountOff20Percent : Discount
    {
        public override decimal DiscountValue()
        {
            return _line.Subtotal * 0.2m;
        }

        public override void Addline(Line line)
        {
            _line = line;
        }
    }
}

