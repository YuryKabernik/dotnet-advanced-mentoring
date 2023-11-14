using CartingService.Abstractions.Interfaces;
using CartingService.BusinessLogic.Interfaces.Handlers;
using CartingService.DataAccess.Entities;
using CartingService.DataAccess.ValueObjects;

namespace CartingService.BusinessLogic.Queries;

public record ItemsRequest(string CartId);

public class AllItemsQuery : IQueryHandler<ItemsRequest, IEnumerable<Item>>
{
    private readonly IRepository<Cart> cartRepository;

    public AllItemsQuery(IRepository<Cart> cartRepository)
    {
        this.cartRepository = cartRepository;
    }

    public async Task<IEnumerable<Item>> Execute(ItemsRequest request, CancellationToken cancellationToken)
    {
        Cart cart = await cartRepository.GetAsync(request.CartId);

        return cart.Items.Values.ToList();
    }
}
