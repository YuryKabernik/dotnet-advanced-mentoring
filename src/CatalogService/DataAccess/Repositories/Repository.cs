using System.Linq.Expressions;
using CatalogService.Contracts.Interfaces;
using LinqToDB;

namespace CatalogService.DataAccess.Repositories;

public class Repository<TSource> : IRepository<TSource> where TSource : class
{
    private readonly DbContext<TSource> context;

    public Repository(DbContext<TSource> context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<TSource>> Get(Expression<Func<TSource, bool>> predicate, int? top = default)
    {
        var query = this.context.Table.Where(predicate);

        if (top.HasValue)
        {
            query.Take(top.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<TSource?> GetFirst(Expression<Func<TSource, bool>> predicate)
    {
        return await this.context.Table.FirstOrDefaultAsync(predicate);
    }

    public async Task Add(TSource entry) => await this.context.InsertAsync(entry);

    public async Task Delete(TSource entry) => await this.context.DeleteAsync(entry);

    public async Task Update(TSource entry) => await this.context.UpdateAsync(entry);

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
            this.context.Dispose();
        }

        this.disposed = true;
    }
}
