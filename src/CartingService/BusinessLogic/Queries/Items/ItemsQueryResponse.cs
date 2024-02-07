using CartingService.DataAccess.Entities;
using CartingService.DataAccess.ValueObjects;

namespace CartingService.BusinessLogic.Queries.Items;

public record ItemsQueryResponse(IEnumerable<Item> Items)
{
    internal static ItemsQueryResponse From(Cart cart) => new ItemsQueryResponse(cart.Items.Values.ToList());
}
