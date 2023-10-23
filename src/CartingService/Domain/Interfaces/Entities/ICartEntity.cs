using CartingService.Domain.ValueObjects;

namespace CartingService.Domain.Interfaces.Entities;

public interface ICartEntity
{
    Item? Get(int itemId);
    bool Add(Item item);
    bool Remove(int itemId);
    IReadOnlyCollection<Item> List();
}