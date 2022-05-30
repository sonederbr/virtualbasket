namespace Domain.Services
{
    using Domain.Models;

    public class BasketService : IBasketOperationService, IBasketCreationService
    {
        public Basket CreateBasket(Guid customerId)
        {
            return Basket.CreateBasket(customerId);
        }

        public Line AddLine(Basket basket, Item item, int quantity)
        {
            if (!item.IsValid())
                throw new Exception("Invalid");
            
            var line = basket.AddItem(item, quantity);

            basket.CalculateTotalValue();

            return line;
        }

        public void RemoveLine(Basket basket, Item item)
        {
            basket.RemoveItem(item);

            basket.CalculateTotalValue();
        }

        public bool IsValid(Basket basket) => basket.IsValid();
    }
}
