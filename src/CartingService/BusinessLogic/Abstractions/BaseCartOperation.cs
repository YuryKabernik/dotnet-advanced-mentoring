using CartingService.BusinessLogic.Exceptions;
using CartingService.BusinessLogic.Interfaces.Ports;
using CartingService.DataAccess.Interfaces;

namespace CartingService.BusinessLogic;

public abstract class BaseCartOperation
{
    protected ICartRepository cartRepository;

    protected async Task<ICartEntity> GetCart(Guid guid)
    {
        if (guid.Equals(Guid.Empty))
        {
            throw new ArgumentException("Empty Guid is not allowed to lookup the cart.");
        }

        return await this.cartRepository.GetCart(guid)
            ?? throw new CartLookupException($"Cart <{guid}> lookup failed.");
    }
}
