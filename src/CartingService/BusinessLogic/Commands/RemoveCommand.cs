using CartingService.BusinessLogic.Exceptions;
using CartingService.BusinessLogic.Interfaces;
using CartingService.Domain.Etities;
using CartingService.Domain.Interfaces.Ports;
using CartingService.Domain.ValueObjects;

namespace CartingService.BusinessLogic.Commands;

public record RemoveRequest(Guid CartId, int ItemId);

public class RemoveCommand : ICommandHandler<RemoveRequest>
{
    private ICartRepository cartRepository;

    public RemoveCommand(ICartRepository cartRepository)
    {
        this.cartRepository = cartRepository;
    }

    public async Task Execute(RemoveRequest request, CancellationToken cancellationToken)
    {
        Cart cart = await this.GetCart(request.CartId);
        Item? item = cart.Get(request.ItemId)
            ?? throw new CommandFailedException($"Item <{request.ItemId}> lookup failed.");

        cart.Remove(item);

        await this.cartRepository.SaveChanges();
    }

    private async Task<Cart> GetCart(Guid guid)
    {
        return await this.cartRepository.GetCart(guid)
            ?? throw new CommandFailedException($"Cart <{guid}> lookup failed.");
    }
}
