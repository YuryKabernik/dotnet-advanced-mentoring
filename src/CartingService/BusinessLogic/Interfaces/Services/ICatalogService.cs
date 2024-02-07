using CartingService.DataAccess.ValueObjects;

namespace CartingService.BusinessLogic.Interfaces.Services;

public interface ICatalogService
{
    Task<Item?> GetItem(string id, CancellationToken cancellation);
}
