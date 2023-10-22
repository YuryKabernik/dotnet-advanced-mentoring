using CartingService.BusinessLogic.Exceptions;
using CartingService.BusinessLogic.Interfaces;
using CartingService.BusinessLogic.Interfaces.Services;
using CartingService.Domain.Interfaces.Entities;
using CartingService.Domain.Interfaces.Ports;
using CartingService.Domain.ValueObjects;

namespace CartingService.BusinessLogic.Commands;

public record NewItem(int Id, int Quantity);
public record AddRequest(Guid CartId, NewItem Item);

public class AddCommand : ICommandHandler<AddRequest>
{
    private ICartRepository cartRepository;
    private ICatalogService catalogService;

    public AddCommand(ICartRepository cartRepository, ICatalogService catalogService)
    {
        this.cartRepository = cartRepository;
        this.catalogService = catalogService;
    }

    public async Task Execute(AddRequest request, CancellationToken cancellationToken)
    {
        ICartEntity? cart = await this.GetCart(request.CartId);
        Item? selectedItem = await this.GetSelectedItem(request.Item);

        this.UpdateQuantity(selectedItem, request);
        cart.Add(selectedItem);

        await this.cartRepository.SaveChanges();
    }

    private void UpdateQuantity(Item selectedItem, AddRequest request)
    {
        selectedItem.Quantity = request.Item.Quantity;
    }

    private async Task<ICartEntity> GetCart(Guid guid)
    {
        return await this.cartRepository.GetCart(guid)
            ?? throw new CommandFailedException($"Cart <{guid}> lookup failed.");
    }

    private async Task<Item> GetSelectedItem(NewItem newItem)
    {
        return await this.catalogService.GetItem(newItem.Id)
            ?? throw new CommandFailedException($"Item <{newItem.Id}> lookup failed.");
    }
}
