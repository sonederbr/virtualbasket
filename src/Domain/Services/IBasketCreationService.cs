namespace Domain.Services
{
    using Domain.Models;

    public interface IBasketCreationService
    {
        Basket CreateBasket(Guid customerId);

        bool IsValid(Basket basket);
    }
}