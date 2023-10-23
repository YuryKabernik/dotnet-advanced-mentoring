﻿using CartingService.BusinessLogic.Exceptions;
using CartingService.BusinessLogic.Interfaces;
using CartingService.Domain.Interfaces.Entities;
using CartingService.Domain.Interfaces.Ports;

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
        ICartEntity cart = await this.GetCart(request.CartId);
        bool isRemoved = cart.Remove(request.ItemId);

        if (!isRemoved)
        {
            throw new CommandFailedException($"Item <{request.ItemId}> deletion failed.");
        }

        await this.cartRepository.SaveChanges();
    }

    private async Task<ICartEntity> GetCart(Guid guid)
    {
        return await this.cartRepository.GetCart(guid)
            ?? throw new CommandFailedException($"Cart <{guid}> lookup failed.");
    }
}
