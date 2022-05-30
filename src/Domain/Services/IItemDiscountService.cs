namespace Domain.Services
{
    using Domain.Services.DiscountRules;

    public interface IItemDiscountService
    {
        Task<Discount> GetDiscountRule(string name);
    }
}
