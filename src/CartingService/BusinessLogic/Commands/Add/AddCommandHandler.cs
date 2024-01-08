using CartingService.Abstractions.Interfaces;
using CartingService.BusinessLogic.Exceptions;
using CartingService.BusinessLogic.Interfaces.Handlers;
using CartingService.BusinessLogic.Interfaces.Services;
using CartingService.DataAccess.Entities;
using CartingService.DataAccess.ValueObjects;

namespace CartingService.BusinessLogic.Commands.Add;

public class AddCommandHandler : ICommandHandler<AddCommandRequest>
{
    private ICatalogService catalogService;
    private readonly IRepository<Cart> cartRepository;

    public AddCommandHandler(IRepository<Cart> cartRepository, ICatalogService catalogService)
    {
        this.cartRepository = cartRepository;
        this.catalogService = catalogService;
    }

    public async Task Handle(AddCommandRequest request, CancellationToken cancellationToken)
    {
        Cart cart = await this.cartRepository.GetAsync(request.CartId);
        Item selectedItem = await this.GetSelectedItem(request.Item, cancellationToken);

        bool isAdded = cart.Items.TryAdd(selectedItem.Id, selectedItem);

        if (!isAdded)
        {
            await this.cartRepository.UpdateAsync(cart);
        }
    }

    private async Task<Item> GetSelectedItem(NewItem newItem, CancellationToken cancellation)
    {
        return await this.catalogService.GetItem(newItem.Id, cancellation)
            ?? throw new CommandFailedException($"Item <{newItem.Id}> lookup failed.");
    }
}
