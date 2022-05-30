namespace Domain.Services.DiscountRules
{
    using Domain.Models;

    public class NoDiscount : Discount
    {
        public override decimal DiscountValue()
        {
            return 0;
        }

        public override void Addline(Line line)
        {
            _line = line;
        }
    }
}
