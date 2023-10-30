using CartingService.DataAccess.Interfaces;

namespace CartingService.BusinessLogic;

public abstract class BaseCartOperation
{
    protected ICartRepository cartRepository;

    protected ICartEntity GetCart(Guid guid)
    {
        if (guid.Equals(Guid.Empty))
        {
            throw new ArgumentException("Empty Guid is not allowed to lookup the cart.");
        }

        return this.cartRepository.GetCart(guid);
    }
}
