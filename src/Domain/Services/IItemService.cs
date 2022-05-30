namespace Domain
{
    using Domain.Models;

    public interface IItemService
    {
        Task<Item> GetItem(string name);
    }
}
