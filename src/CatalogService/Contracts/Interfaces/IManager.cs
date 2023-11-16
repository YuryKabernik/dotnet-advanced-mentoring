namespace CatalogService.Contracts.Interfaces;

public interface IManager<TEntity> : IDisposable
{
    Task AddAsync(TEntity entity);
    Task DeleteAsync(string id);
    Task<TEntity?> GetAsync(string id);
    Task<IEnumerable<TEntity>> GetAsync();
    Task UpdateAsync(TEntity newEntity);
}
