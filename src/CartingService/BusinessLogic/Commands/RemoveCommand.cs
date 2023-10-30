using CartingService.BusinessLogic.Exceptions;
using CartingService.BusinessLogic.Interfaces.Handlers;
using CartingService.DataAccess.Interfaces;

namespace CartingService.BusinessLogic.Commands;

public record RemoveRequest(Guid CartId, int ItemId);

public class RemoveCommand : BaseCartOperation, ICommandHandler<RemoveRequest>
{
    public RemoveCommand(ICartRepository cartRepository)
    {
        this.cartRepository = cartRepository;
    }

    public async Task Execute(RemoveRequest request, CancellationToken cancellationToken)
    {
        ICartEntity cart = this.GetCart(request.CartId);
        bool isRemoved = await cart.Remove(request.ItemId);

        if (!isRemoved)
        {
            throw new CommandFailedException($"Item <{request.ItemId}> deletion failed.");
        }
    }
}
