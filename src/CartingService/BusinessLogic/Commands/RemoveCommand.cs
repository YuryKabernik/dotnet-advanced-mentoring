using CartingService.Abstractions.Interfaces;
using CartingService.BusinessLogic.Exceptions;
using CartingService.BusinessLogic.Interfaces.Handlers;
using CartingService.DataAccess.Entities;
using Microsoft.Extensions.Logging;

namespace CartingService.BusinessLogic.Commands;

public record RemoveRequest(string CartId, string ItemId);

public class RemoveCommand : ICommandHandler<RemoveRequest>
{
    private readonly ILogger<RemoveCommand> logger;
    private readonly IRepository<Cart> cartRepository;

    public RemoveCommand(IRepository<Cart> cartRepository, ILogger<RemoveCommand> logger)
    {
        this.logger = logger;
        this.cartRepository = cartRepository;
    }

    public async Task Execute(RemoveRequest request, CancellationToken cancellationToken)
    {
        Cart cart = await cartRepository.GetAsync(request.CartId);
        bool isRemoved = cart.Items.Remove(request.ItemId);

        if (!isRemoved)
        {
            this.logger.LogError("Failed to delete an item '{0}' in a cart '{1}'", request.ItemId, request.CartId);
            throw new CommandFailedException($"Item '{request.ItemId}' deletion failed.");
        }

        await cartRepository.UpdateAsync(cart);

        this.logger.LogError("Deleted an item '{0}' in a cart '{1}'", request.ItemId, cart.RawId);
    }
}
