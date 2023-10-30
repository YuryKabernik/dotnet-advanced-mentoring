using CartingService.DataAccess.ValueObjects;

namespace CartingService.DataAccess.Interfaces;

public interface ICartEntity
{
    Task<Item?> Get(int itemId);
    Task<bool> Add(Item item);
    Task<bool> Remove(int itemId);
    Task<IEnumerable<Item>> List();
    Task Update(Item selectedItem);
}