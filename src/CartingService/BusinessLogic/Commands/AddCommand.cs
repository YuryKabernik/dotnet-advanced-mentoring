using CartingService.Abstractions.Interfaces;
using CartingService.BusinessLogic.Exceptions;
using CartingService.BusinessLogic.Interfaces.Handlers;
using CartingService.BusinessLogic.Interfaces.Services;
using CartingService.DataAccess.Entities;
using CartingService.DataAccess.ValueObjects;

namespace CartingService.BusinessLogic.Commands;

public record NewItem(string Id, int Quantity);
public record AddRequest(string CartId, NewItem Item);

public class AddCommand : ICommandHandler<AddRequest>
{
    private ICatalogService catalogService;
    private readonly IRepository<Cart> cartRepository;

    public AddCommand(IRepository<Cart> cartRepository, ICatalogService catalogService)
    {
        this.cartRepository = cartRepository;
        this.catalogService = catalogService;
    }

    public async Task Execute(AddRequest request, CancellationToken cancellationToken)
    {
        Cart cart = await this.cartRepository.GetAsync(request.CartId);
        Item selectedItem = await this.GetSelectedItem(request.Item);

        bool isAdded = cart.Items.TryAdd(selectedItem.Id, selectedItem);

        if (!isAdded)
        {
            await this.cartRepository.UpdateAsync(cart);
        }
    }

    private async Task<Item> GetSelectedItem(NewItem newItem)
    {
        return await this.catalogService.GetItem(newItem.Id)
            ?? throw new CommandFailedException($"Item <{newItem.Id}> lookup failed.");
    }
}
