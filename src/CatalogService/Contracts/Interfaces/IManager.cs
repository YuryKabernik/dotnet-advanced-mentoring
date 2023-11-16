namespace CatalogService.Contracts.Interfaces;

public interface IManager<TEntity>
{
    Task AddAsync(TEntity entity);
    Task DeleteAsync(string id);
    Task<TEntity?> GetAsync(string id);
    Task<IEnumerable<TEntity>> GetAsync();
    Task UpdateAsync(string id, TEntity newEntity);
}
