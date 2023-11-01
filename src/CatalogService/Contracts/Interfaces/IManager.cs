namespace CatalogService.Contracts;

public interface IManager<TEntity>
{
    Task AddAsync(TEntity entity);
    Task DeleteAsync(int id);
    Task<TEntity?> GetAsync(int id);
    Task<IEnumerable<TEntity>> ListAsync();
    Task UpdateAsync(int id, TEntity newEntity);
}
