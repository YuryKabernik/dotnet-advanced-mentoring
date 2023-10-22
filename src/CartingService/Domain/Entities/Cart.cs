using CartingService.Domain.Interfaces.Entities;
using CartingService.Domain.Validation;
using CartingService.Domain.ValueObjects;

namespace CartingService.Domain.Etities
{
    public class Cart : ICartEntity
    {
        public Guid Guid { get; set; }
        public IDictionary<int, Item> Items { get; set; } = new Dictionary<int, Item>();

        public Item? Get(int itemId)
        {
            this.Items.TryGetValue(itemId, out Item? item);

            return item;
        }

        public void Add(Item item)
        {
            ValidateItem(item);

            bool successfullyAdded = this.Items.TryAdd(item.Id, item);
            if (!successfullyAdded)
            {
                this.Items[item.Id].Quantity = item.Quantity;
            }
        }

        public void Remove(Item item)
        {
            ValidateItem(item);
            this.Items.Remove(item.Id);
        }

        private static void ValidateItem(Item item)
        {
            ValidationResult result = item.Validate();
            if (result.IsValid is not true)
            {
                throw new ArgumentException(
                    $"Invalid {result.PropertyName} property of the item."
                );
            }
        }

        public IReadOnlyList<Item> List()
        {
            return Items.Select(i => i.Value).ToList();
        }
    }
}
