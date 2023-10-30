using CartingService.BusinessLogic.Interfaces.Handlers;
using CartingService.DataAccess.Interfaces;
using CartingService.DataAccess.ValueObjects;

namespace CartingService.BusinessLogic.Queries;

public record ItemsRequest(Guid CartId);

public class AllItemsQuery : BaseCartOperation, IQueryHandler<ItemsRequest, IEnumerable<Item>>
{
    public AllItemsQuery(ICartRepository cartRepository)
    {
        this.cartRepository = cartRepository;
    }

    public async Task<IEnumerable<Item>> Execute(ItemsRequest request, CancellationToken cancellationToken)
    {
        ICartEntity cart = this.GetCart(request.CartId);

        return await cart.List();
    }
}
