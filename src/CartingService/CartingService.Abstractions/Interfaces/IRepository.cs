namespace CartingService.Abstractions.Interfaces;

public interface IRepository<TEntity> where TEntity : IDocumentEntity
{
    Task<IEnumerable<TEntity>> GetAsync();
    Task<TEntity> GetAsync(string id);
    Task AddAsync(TEntity item);
    Task UpdateAsync(TEntity item);
    Task RemoveAsync(string id);
}
