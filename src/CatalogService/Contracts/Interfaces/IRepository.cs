using System.Linq.Expressions;

namespace CatalogService.Contracts.Interfaces;

public interface IRepository<TSource> : IDisposable
{
    Task Add(TSource entry);
    Task Delete(TSource entry);
    Task<TSource?> GetFirst(Expression<Func<TSource, bool>> predicate);
    Task<IEnumerable<TSource>> Get(Expression<Func<TSource, bool>> predicate, int? top = default);
    Task Update(TSource entry);
}
