using CartingService.Domain.ValueObjects;

namespace CartingService.Domain.Etities
{
    public class Cart
    {
        public Guid Guid { get; set; }
        public ICollection<Item> Items { get; } = new List<Item>();
    }
}
