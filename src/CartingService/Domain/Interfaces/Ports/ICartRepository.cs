using CartingService.Domain.Etities;

namespace CartingService.Domain.Interfaces.Ports;

public interface ICartRepository
{
    Task<Cart> GetCart(Guid guid);
    Task SaveChanges();
}
