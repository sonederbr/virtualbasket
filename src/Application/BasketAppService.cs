namespace Application
{
    using Application.Dtos;

    using Domain;
    using Domain.Models;
    using Domain.Services;

    public class BasketAppService : IBasketAppService
    {
        private readonly IBasketOperationService _basketOperationService;
        private readonly IBasketCreationService _basketCreationService; 
        private readonly IItemService _itemService;
        private readonly IItemDiscountService _itemDiscountService;

        public BasketAppService(
            IBasketOperationService basketOperationService,
            IBasketCreationService basketCreationService,
            IItemService itemService,
            IItemDiscountService itemDiscountService)
        {
            _basketOperationService = basketOperationService;
            _basketCreationService = basketCreationService;
            _itemService = itemService;
            _itemDiscountService = itemDiscountService;
        }

        public async Task<BasketDto> CreateShoppingBag(Guid customerId, IEnumerable<ItemDto> itemsDto)
        {
            var basket = _basketCreationService.CreateBasket(customerId);

            await CreateLines(itemsDto, basket);

            await ApplyDiscounts(basket);

            _basketCreationService.IsValid(basket);

            return new BasketDto
            {
                Total = basket.Total,
                Discount = basket.Discount,
                Items = basket.Lines.Select(p =>
                {
                    return new ItemDto { Name = p.Item.Name, Price = p.Item.Price, Quantity = p.Quantity };
                })
            };
        }

        private async Task CreateLines(IEnumerable<ItemDto> itemsDto, Basket basket)
        {
            foreach (var itemDto in itemsDto)
            {
                var item = await _itemService.GetItem(itemDto.Name);
                _basketOperationService.AddLine(basket, item, itemDto.Quantity);
            }
        }

        private async Task ApplyDiscounts(Basket basket)
        {
            foreach (var line in basket.Lines)
            {
                var discountRule = await _itemDiscountService.GetDiscountRule(line.Item.Name);
                basket.AddDiscount(line, discountRule.DiscountValue());
            }
        }
    }
}
