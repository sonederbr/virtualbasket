namespace ApplicationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Application;
    using Application.Dtos;

    using ApplicationTests.ClassData;

    using Domain;
    using Domain.Models;
    using Domain.Services;
    using Domain.Services.DiscountRules;

    using FluentAssertions;

    using Moq;

    using Xunit;

    public class BasketAppServiceTest
    {
        private readonly BasketAppService _basketAppService;
        private readonly Mock<IBasketOperationService> _basketOperationServiceMock;
        private readonly Mock<IBasketCreationService> _basketCreationServiceMock;
        private readonly Mock<IItemService> _itemServiceMock;
        private readonly Mock<IItemDiscountService> _itemDiscountServiceMock;

        public BasketAppServiceTest()
        {
            _basketOperationServiceMock = new Mock<IBasketOperationService>();
            _basketCreationServiceMock = new Mock<IBasketCreationService>();
            _itemServiceMock = new Mock<IItemService>();
            _itemDiscountServiceMock = new Mock<IItemDiscountService>();

            _basketAppService = new BasketAppService(
                _basketOperationServiceMock.Object, 
                _basketCreationServiceMock.Object, 
                _itemServiceMock.Object, 
                _itemDiscountServiceMock.Object);
        }

        [Theory]
        [ClassData(typeof(ReturnBasketClassData))]
        public async Task Create_New_Basket_And_Add_line_With_10_Percent_Off_Discount_Calculated(
            List<ItemDto> itemsDto, int total, int discount, int countItems, string duplicatedItem, int totalDuplicatedItem)
        {
            // arrange
            var customerId = Guid.NewGuid();
            
            var basket = Basket.CreateBasket(customerId);

            _basketCreationServiceMock.Setup(p => p.CreateBasket(customerId)).Returns(basket);
           
            foreach (var itemDto in itemsDto)
            {
                var item = Item.CreateItem(itemDto.Name, itemDto.Price);
                var line = Line.CreateLine(item, itemDto.Quantity);
                basket.AddItem(line.Item, line.Quantity);

                Discount discountRule = SimulateDiscountRule(itemDto);
                discountRule.Addline(line);
                
                _basketOperationServiceMock.Setup(p => p.AddLine(basket, item, line.Quantity)).Returns(line);

                _itemServiceMock.Setup(p => p.GetItem(item.Name)).ReturnsAsync(item);
                _itemDiscountServiceMock.Setup(p => p.GetDiscountRule(item.Name)).ReturnsAsync(discountRule);
            }

            // act
            var basketDto = await _basketAppService.CreateShoppingBag(customerId, itemsDto);

            // assert
            basketDto.Total.Should().Be(total);
            basketDto.Discount.Should().Be(discount);
            basketDto.Items.Count().Should().Be(countItems);
            if (!string.IsNullOrWhiteSpace(duplicatedItem))
                basketDto.Items.Single(p => p.Name == duplicatedItem).Quantity.Should().Be(totalDuplicatedItem);
        }

        private static Discount SimulateDiscountRule(ItemDto item)
        {
            return item.Name switch
            {
                "Pineapple" => new DiscountOff10Percent(),
                "Plum" => new DiscountOff20Percent(),
                "Banana" => new DiscountBuyTwicePayOne(),
                "Pear" => new DiscountBuyTwicePayOne(),
                _ => new NoDiscount()
            };
        }
    }
}