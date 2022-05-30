namespace Domain.Services
{
    using Domain.Models;

    public interface IBasketOperationService
    {
        Line AddLine(Basket basket, Item item, int quantity);

        void RemoveLine(Basket basket, Item item);
    }
}