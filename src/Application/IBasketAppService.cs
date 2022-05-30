namespace Application
{
    using Application.Dtos;

    public interface IBasketAppService
    {
        Task<BasketDto> CreateShoppingBag(Guid customerId, IEnumerable<ItemDto> items);
    }
}