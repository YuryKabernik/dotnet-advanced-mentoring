using Ardalis.Specification;
using CatalogService.Domain.Contracts.Entities;

namespace CatalogService.Domain.Contracts.Interfaces;

public interface IRepository<TSource> : IRepositoryBase<TSource>
    where TSource : Entity<Guid>
{
}
