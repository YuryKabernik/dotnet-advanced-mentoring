using CartingService.DataAccess.Interfaces;

namespace CartingService.BusinessLogic.Interfaces.Ports;

public interface ICartRepository
{
    Task<ICartEntity> GetCart(Guid guid);
    Task SaveChanges(CancellationToken token);
}
