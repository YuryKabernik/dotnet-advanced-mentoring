using CartingService.BusinessLogic.Exceptions;
using CartingService.BusinessLogic.Interfaces;
using CartingService.Domain.Interfaces.Entities;
using CartingService.Domain.Interfaces.Ports;
using CartingService.Domain.ValueObjects;

namespace CartingService.BusinessLogic.Queries;

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
        ICartEntity cart = await this.GetCart(request.CartId);

        return cart.List();
    }

    private async Task<ICartEntity> GetCart(Guid guid)
    {
        if (guid.Equals(Guid.Empty))
        {
            throw new ArgumentException("Empty Guid is not allowed to lookup the cart.");
        }

        var cart = await this.cartRepository.GetCart(guid);

        return cart ?? throw new QueryFailedException($"Cart <{guid}> lookup failed.");
    }
}
