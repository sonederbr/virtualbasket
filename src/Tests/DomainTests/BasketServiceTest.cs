namespace DomainTests
{
    using System;
    using System.Linq;

    using Domain.Models;
    using Domain.Services;
    using Domain.Services.DiscountRules;

    using FluentAssertions;

    using Xunit;

    public class BasketServiceTest
    {
        private readonly Guid _customerId;

        private BasketService _basketService;
        public BasketServiceTest()
        {
            _customerId = Guid.NewGuid();

            _basketService = new BasketService();
        }

        [Fact]
        public void Create_New_Basket_With_Customer_Total_Is_Zero()
        {
            // act
            var basket = _basketService.CreateBasket(_customerId);

            // assert
            basket.CustomerId.Should().Be(_customerId);
            basket.Total.Should().Be(0);
            basket.Lines.Count().Should().Be(0);
        }

        [Fact]
        public void Create_New_Basket_And_Add_One_Line_Total_Is_Calculated()
        {
            // arrange
            var basket = _basketService.CreateBasket(_customerId);

            // act
            _basketService.AddLine(basket, Item.CreateItem("Orange", 10m), 2);

            // assert
            basket.CustomerId.Should().Be(_customerId);
            basket.Total.Should().Be(20m);
            basket.Lines.Count().Should().Be(1);
        }

        [Fact]
        public void Create_New_Basket_And_Add_One_Line_And_Add_Buy_Twice_Pay_One_And_Apply_Discount_Total_And_Discount_Is_Calculated()
        {
            // arrange
            var basket = _basketService.CreateBasket(_customerId);

            var line = _basketService.AddLine(basket, Item.CreateItem("Orange", 10m), 9);

            // act
            Discount discountRule = new DiscountBuyTwicePayOne();
            discountRule.Addline(line);
            basket.AddDiscount(line, discountRule.DiscountValue());

            // assert
            basket.CustomerId.Should().Be(_customerId);
            basket.Total.Should().Be(50m);
            basket.Discount.Should().Be(40m);
            basket.Lines.Count().Should().Be(1);
        }

        [Fact]
        public void Create_New_Basket_And_Add_One_Line_And_Add_Discount_Off_10_And_Apply_Discount_Total_And_Discount_Is_Calculated()
        {
            // arrange
            var basket = _basketService.CreateBasket(_customerId);

            var line = _basketService.AddLine(basket, Item.CreateItem("Orange", 10m), 2);

            // act
            Discount discountRule = new DiscountOff10Percent();
            discountRule.Addline(line);
            basket.AddDiscount(line, discountRule.DiscountValue());

            // assert
            basket.CustomerId.Should().Be(_customerId);
            basket.Total.Should().Be(18m);
            basket.Discount.Should().Be(2m);
            basket.Lines.Count().Should().Be(1);
        }

        [Fact]
        public void Create_New_Basket_And_Add_One_Line_And_Add_Discount_Off_20_And_Apply_Discount_Total_And_Discount_Is_Calculated()
        {
            // arrange
            var basket = _basketService.CreateBasket(_customerId);

            var line = _basketService.AddLine(basket, Item.CreateItem("Orange", 10m), 2);

            // act
            Discount discountRule = new DiscountOff20Percent();
            discountRule.Addline(line);
            basket.AddDiscount(line, discountRule.DiscountValue());

            // assert
            basket.CustomerId.Should().Be(_customerId);
            basket.Total.Should().Be(16m);
            basket.Discount.Should().Be(4m);
            basket.Lines.Count().Should().Be(1);
        }

        [Fact]
        public void Create_New_Basket_And_Add_Two_Lines_And_Remove_One_Line_Line_Is_Removed_And_Total_Is_Calculated()
        {
            // arrange
            var basket = _basketService.CreateBasket(_customerId);

            var orange = Item.CreateItem("Orange", 10m);
            _basketService.AddLine(basket, orange, 2);
            var apple = Item.CreateItem("Apple", 5m);
            _basketService.AddLine(basket, apple, 2);

            // act
            _basketService.RemoveLine(basket, orange);

            // assert
            basket.CustomerId.Should().Be(_customerId);
            basket.Total.Should().Be(10m);
            basket.Lines.Count().Should().Be(1);
        }

        [Fact]
        public void Create_New_Basket_And_Add_Two_Lines_And_The_Same_Item_Total_Items_Is_The_Sum_Of()
        {
            // arrange
            _basketService = new BasketService();
            var basket = _basketService.CreateBasket(_customerId);

            // act
            var orange = Item.CreateItem("Apple", 10m);
            _basketService.AddLine(basket, orange, 2);
            var apple = Item.CreateItem("Apple", 10m);
            _basketService.AddLine(basket, apple, 2);

            // assert
            basket.CustomerId.Should().Be(_customerId);
            basket.Total.Should().Be(40m);
            basket.Lines.Count().Should().Be(1);
        }
    }
}
