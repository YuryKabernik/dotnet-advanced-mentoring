using System.Buffers;
using CartingService.Domain.Interfaces.Entities;
using CartingService.Domain.Validation;
using CartingService.Domain.ValueObjects;

namespace CartingService.Domain.Etities
{
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
            ValidateItem(item);

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

        private static void ValidateItem(Item item)
        {
            ValidationResult r = item.Validate();

            if (!r.IsValid)
            {
                throw new ArgumentException($"Invalid {r.PropertyName} property of the item.");
            }
        }
    }
}
