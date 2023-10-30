namespace CartingService.DataAccess.Interfaces;

public interface ICartRepository
{
    ICartEntity GetCart(Guid guid);
}
