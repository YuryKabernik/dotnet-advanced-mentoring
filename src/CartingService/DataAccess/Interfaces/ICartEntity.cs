using CartingService.DataAccess.ValueObjects;

namespace CartingService.DataAccess.Interfaces;

public interface ICartEntity
{
    Item? Get(int itemId);
    bool Add(Item item);
    bool Remove(int itemId);
    IReadOnlyCollection<Item> List();
}