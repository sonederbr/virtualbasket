namespace ApplicationTests.ClassData
{
    using System.Collections;
    using System.Collections.Generic;

    using Application.Dtos;

    public class ReturnBasketClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new List<ItemDto>
            {
                new ItemDto { Name = "Orange", Quantity = 1, Price = 10m }
            }, 10, 0, 1, string.Empty, 0  };

            yield return new object[] { new List<ItemDto>
            {
                new ItemDto{ Name = "Orange", Quantity = 1, Price = 10m },
                new ItemDto{ Name = "Orange", Quantity = 1, Price = 10m },
                new ItemDto{ Name = "Apple", Quantity = 1, Price = 5m },
            }, 25, 0, 2, "Orange", 2};

            yield return new object[] { new List<ItemDto>
            {
                new ItemDto{ Name = "Grape", Quantity = 2, Price = 15m },
                new ItemDto{ Name = "Orange", Quantity = 2, Price = 10m },
                new ItemDto{ Name = "Apple", Quantity = 2, Price = 5m },
            }, 60, 0, 3, string.Empty, 0};

            yield return new object[] { new List<ItemDto>
            {
                new ItemDto { Name = "Pineapple", Quantity = 1, Price = 10m }
            }, 9, 1, 1, string.Empty, 0};

            yield return new object[] { new List<ItemDto>
            {
                new ItemDto { Name = "Plum", Quantity = 1, Price = 10m }
            }, 8, 2, 1, string.Empty, 0};

            yield return new object[] { new List<ItemDto>
            {
                new ItemDto{ Name = "Banana", Quantity = 2, Price = 10m },
                new ItemDto{ Name = "Apple", Quantity = 1, Price = 5m },
            }, 15, 10, 2, string.Empty, 0};

            yield return new object[] { new List<ItemDto>
            {
                new ItemDto{ Name = "Banana", Quantity = 9, Price = 10m },
                new ItemDto{ Name = "Apple", Quantity = 1, Price = 5m },
            }, 55, 40, 2, string.Empty, 0};

            yield return new object[] { new List<ItemDto>
            {
                new ItemDto{ Name = "Strawberry", Quantity = 2, Price = 15m }
            }, 30, 0, 1, string.Empty, 0};

            yield return new object[] { new List<ItemDto>
            {
                new ItemDto{ Name = "Pear", Quantity = 2, Price = 15m },
                new ItemDto { Name = "Pineapple", Quantity = 2, Price = 10m },
                new ItemDto { Name = "Plum", Quantity = 4, Price = 10m }
            }, 65, 25, 3, string.Empty, 0};
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
