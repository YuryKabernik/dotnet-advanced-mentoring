using CatalogService.Domain.Contracts.Entities;
using CatalogService.Domain.Contracts.Interfaces;

namespace CatalogService.DataAccess.Repositories;

public abstract class BaseRepository<TSource, TEntityId> : IRepository<TSource, TEntityId>
    where TSource : Entity<TEntityId>
{
    protected readonly SourceContext Context;

    protected BaseRepository(SourceContext context)
    {
        this.Context = context;
    }

    public abstract Task<IEnumerable<TSource>> GetAllAsync();

    public abstract Task<TSource?> GetSingleAsync(TEntityId guid);

    public Task AddAsync(TSource entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        this.Context.Add(entry);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(TSource entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        this.Context.Remove(entry);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(TSource entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        this.Context.Update(entry);

        return Task.CompletedTask;
    }
}
