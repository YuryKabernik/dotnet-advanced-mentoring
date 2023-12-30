using CatalogService.Domain.Contracts.Entities;

namespace CatalogService.Domain.Contracts.Interfaces;

public interface IRepository<TSource, in TEntityId>
    where TSource : Entity<TEntityId>
{
    Task<TSource?> GetSingleAsync(TEntityId id);
    
    Task<IEnumerable<TSource>> GetAllAsync();
    
    Task AddAsync(TSource entity);
    
    Task DeleteAsync(TSource entity);

    Task UpdateAsync(TSource entity);
}
