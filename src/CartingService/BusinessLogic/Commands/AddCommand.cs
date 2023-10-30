using CartingService.BusinessLogic.Exceptions;
using CartingService.BusinessLogic.Interfaces.Handlers;
using CartingService.BusinessLogic.Interfaces.Services;
using CartingService.DataAccess.Interfaces;
using CartingService.DataAccess.ValueObjects;

namespace CartingService.BusinessLogic.Commands;

public record NewItem(int Id, int Quantity);
public record AddRequest(Guid CartId, NewItem Item);

public class AddCommand : BaseCartOperation, ICommandHandler<AddRequest>
{
    private ICatalogService catalogService;

    public AddCommand(ICartRepository cartRepository, ICatalogService catalogService)
    {
        this.cartRepository = cartRepository;
        this.catalogService = catalogService;
    }

    public async Task Execute(AddRequest request, CancellationToken cancellationToken)
    {
        ICartEntity? cart = this.GetCart(request.CartId);
        Item? selectedItem = await this.GetSelectedItem(request.Item);

        bool isAdded = await cart.Add(selectedItem);

        if (!isAdded)
        {
            await cart.Update(selectedItem);
        }
    }

    private async Task<Item> GetSelectedItem(NewItem newItem)
    {
        return await this.catalogService.GetItem(newItem.Id)
            ?? throw new CommandFailedException($"Item <{newItem.Id}> lookup failed.");
    }
}
