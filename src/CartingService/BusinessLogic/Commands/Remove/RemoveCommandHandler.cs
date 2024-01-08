using CartingService.Abstractions.Interfaces;
using CartingService.BusinessLogic.Exceptions;
using CartingService.BusinessLogic.Interfaces.Handlers;
using CartingService.DataAccess.Entities;
using Microsoft.Extensions.Logging;

namespace CartingService.BusinessLogic.Commands.Remove;

public class RemoveCommandHandler : ICommandHandler<RemoveCommandRequest>
{
    private readonly ILogger<RemoveCommandHandler> logger;
    private readonly IRepository<Cart> cartRepository;

    public RemoveCommandHandler(IRepository<Cart> cartRepository, ILogger<RemoveCommandHandler> logger)
    {
        this.logger = logger;
        this.cartRepository = cartRepository;
    }

    public async Task Handle(RemoveCommandRequest request, CancellationToken cancellationToken)
    {
        Cart cart = await cartRepository.GetAsync(request.CartId);
        bool isRemoved = cart.Items.Remove(request.ItemId);

        if (!isRemoved)
        {
            logger.LogInformation($"Deleted an item '{request.ItemId}' in a cart '{request.CartId}'");
            throw new CommandFailedException($"Item '{request.ItemId}' deletion failed.");
        }

        await cartRepository.UpdateAsync(cart);

        logger.LogInformation($"Deleted an item '{request.ItemId}' in a cart '{cart.RawId}'");
    }
}
