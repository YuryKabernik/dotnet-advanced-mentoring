using CartingService.Abstractions.Interfaces;
using CartingService.BusinessLogic.Interfaces.Handlers;
using CartingService.DataAccess.Entities;

namespace CartingService.BusinessLogic.Queries.Items;

public class ItemsQueryHandler : IQueryHandler<ItemsQueryRequest, ItemsQueryResponse>
{
    private readonly IRepository<Cart> cartRepository;

    public ItemsQueryHandler(IRepository<Cart> cartRepository)
    {
        this.cartRepository = cartRepository;
    }

    public async Task<ItemsQueryResponse> Handle(ItemsQueryRequest request, CancellationToken cancellationToken)
    {
        Cart cart = await this.cartRepository.GetAsync(request.CartId);

        return ItemsQueryResponse.From(cart);
    }
}
