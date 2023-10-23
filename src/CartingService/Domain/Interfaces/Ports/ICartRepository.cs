using CartingService.Domain.Interfaces.Entities;

namespace CartingService.Domain.Interfaces.Ports;

public interface ICartRepository
{
    Task<ICartEntity> GetCart(Guid guid);
    Task SaveChanges(CancellationToken token);
}
