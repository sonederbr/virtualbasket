namespace Domain.Services.DiscountRules
{
    using Domain.Models;

    public class DiscountOff10Percent : Discount
    {
        public override decimal DiscountValue()
        {
            return _line.Subtotal * 0.1m;
        }

        public override void Addline(Line line)
        {
            _line = line;
        }
    }
}
