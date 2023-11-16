using CatalogService.Contracts.Interfaces;

namespace CatalogService.BusinessLogic.Managers;

public abstract class BaseSourceManager<TSource> : IManager<TSource>
{
    protected readonly IRepository<TSource> repository;

    public BaseSourceManager(IRepository<TSource> repository)
    {
        this.repository = repository;
    }

    public abstract Task AddAsync(TSource entity);
    
    public abstract Task DeleteAsync(string id);
    
    public abstract Task<TSource?> GetAsync(string id);
    
    public abstract Task<IEnumerable<TSource>> GetAsync();
    
    public abstract Task UpdateAsync(TSource newEntity);
    
    private bool disposed = false;

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed && disposing)
        {
            this.repository.Dispose();
        }

        this.disposed = true;
    }
}
