namespace Domain.Services.DiscountRules
{
    using Domain.Models;

    public abstract class Discount
    {
        protected Line _line;

        public abstract void Addline(Line line);

        public virtual decimal DiscountValue()
        {
            return 0;
        }
    }
}
