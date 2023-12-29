using CatalogService.Domain.Contracts.Interfaces;

namespace CatalogService.Application.Managers;

public abstract class BaseSourceManager<TSource> : IManager<TSource>
{
    protected readonly IRepository<TSource> _repository;

    protected BaseSourceManager(IRepository<TSource> repository)
    {
        this._repository = repository;
    }

    public abstract Task AddAsync(TSource entity);
    
    public abstract Task DeleteAsync(string id);
    
    public abstract Task<TSource?> GetAsync(string id);
    
    public abstract Task<IEnumerable<TSource>> GetAsync();
    
    public abstract Task UpdateAsync(TSource newEntity);
}
