using System.Buffers;
using CartingService.DataAccess.Interfaces;
using CartingService.DataAccess.ValueObjects;

namespace CartingService.DataAccess.Etities;

public class Cart : ICartEntity
{
    public Guid Guid { get; set; }
    public IDictionary<int, Item> Items { get; set; }

    public Item? Get(int itemId)
    {
        this.Items.TryGetValue(itemId, out Item? item);

        return item;
    }

    public bool Add(Item item)
    {
        return this.Items.TryAdd(item.Id, item);
    }

    public bool Remove(int itemId)
    {
        return this.Items.Remove(itemId);
    }

    public IReadOnlyCollection<Item> List()
    {
        if (this.Items is null)
        {
            return ArrayPool<Item>.Shared.Rent(0);
        }

        return Items.Values.ToList();
    }
}
