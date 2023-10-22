using CartingService.BusinessLogic.Exceptions;
using CartingService.BusinessLogic.Interfaces;
using CartingService.Domain.Etities;
using CartingService.Domain.Interfaces.Ports;
using CartingService.Domain.ValueObjects;

namespace CartingService.BusinessLogic;

public record ItemsRequest(Guid CartId);

public class AllItemsQuery : IQueryHandler<ItemsRequest, IEnumerable<Item>>
{
    private ICartRepository cartRepository;

    public AllItemsQuery(ICartRepository cartRepository)
    {
        this.cartRepository = cartRepository;
    }

    public async Task<IEnumerable<Item>> Execute(ItemsRequest request, CancellationToken cancellationToken)
    {
        Cart cart = await this.GetCart(request.CartId);

        return cart.List();
    }

    private async Task<Cart> GetCart(Guid guid)
    {
        return await this.cartRepository.GetCart(guid)
            ?? throw new QueryFailedException($"Cart <{guid}> lookup failed.");
    }
}
