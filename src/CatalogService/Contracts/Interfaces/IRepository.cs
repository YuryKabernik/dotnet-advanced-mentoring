using System.Linq.Expressions;

namespace CatalogService.Domain.Contracts.Interfaces;

public interface IRepository<TSource>
{
    Task AddAsync(TSource entry);
    Task DeleteAsync(TSource entry);
    Task<TSource?> GetSingleAsync(Expression<Func<TSource, bool>> predicate);
    Task<IEnumerable<TSource>> GetTopAsync(Expression<Func<TSource, bool>> predicate, int? top = default);
    Task UpdateAsync(TSource entry);
}
