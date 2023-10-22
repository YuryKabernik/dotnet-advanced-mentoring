using CartingService.Domain.ValueObjects;

namespace CartingService.Domain.Interfaces.Entities;

public interface ICartEntity
{
    Item? Get(int itemId);
    IReadOnlyList<Item> List();
    void Add(Item item);
    void Remove(Item item);
}