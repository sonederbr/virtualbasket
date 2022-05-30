namespace Application.Dtos
{
    public class BasketDto
    {
        public BasketDto()
        {
            Items = new List<ItemDto>();
        }

        public decimal Total { get; set; }

        public decimal Discount { get; set; }

        public IEnumerable<ItemDto> Items { get; set; }
    }
}
