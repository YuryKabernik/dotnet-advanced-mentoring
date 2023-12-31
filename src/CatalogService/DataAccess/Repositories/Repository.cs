using Ardalis.Specification.EntityFrameworkCore;
using CatalogService.Domain.Contracts.Entities;
using CatalogService.Domain.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DataAccess.Repositories;

public class Repository<TSource> : RepositoryBase<TSource>, IRepository<TSource>
    where TSource : Entity<Guid>
{
    public Repository(DbContext context) : base(context)
    {
    }
}
