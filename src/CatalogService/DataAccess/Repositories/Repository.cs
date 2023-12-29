using System.Linq.Expressions;
using CatalogService.Domain.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DataAccess.Repositories;

public class Repository<TSource> : IRepository<TSource> where TSource : class
{
    private readonly SourceContext _context;

    public Repository(SourceContext context)
    {
        this._context = context;
    }

    public async Task<IEnumerable<TSource>> GetTopAsync(Expression<Func<TSource, bool>> predicate, int? top = default)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        var query = this._context.Set<TSource>().Where(predicate);

        if (top.HasValue)
        {
            query = query.Take(top.Value);
        }

        return await query.ToListAsync();
    }

    public Task<TSource?> GetSingleAsync(Expression<Func<TSource, bool>> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return this._context.Set<TSource>().SingleOrDefaultAsync(predicate);
    }

    public Task AddAsync(TSource entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        this._context.Add(entry);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(TSource entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        this._context.Remove(entry);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(TSource entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        this._context.Update(entry);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
