using Ardalis.Specification;
using CatalogService.Domain.Contracts.Entities;

namespace CatalogService.Domain.Contracts.Interfaces;

public interface IRepository<TSource>
    where TSource : Entity<Guid>
{
    Task<List<TSource>> ListAsync(ISpecification<TSource> spec, CancellationToken cancellationToken = default);
    Task<TSource> AddAsync(TSource entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TSource entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TSource entity, CancellationToken cancellationToken = default);
}
