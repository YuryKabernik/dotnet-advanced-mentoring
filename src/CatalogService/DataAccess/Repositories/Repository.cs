using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using CatalogService.Domain.Contracts.Entities;
using CatalogService.Domain.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DataAccess.Repositories;

public class Repository<TSource> : IRepository<TSource>
    where TSource : Entity<Guid>
{
    private readonly DbContext _dbContext;
    private readonly ISpecificationEvaluator _specificationEvaluator;

    public Repository(DbContext context)
    {
        this._dbContext = context;
        this._specificationEvaluator = SpecificationEvaluator.Default;
    }

    public Task<List<TSource>> ListAsync(ISpecification<TSource> spec, CancellationToken cancellationToken = default)
    {
        var source = this._dbContext.Set<TSource>().AsQueryable();
        var query = this._specificationEvaluator.GetQuery(source, spec);

        return query.ToListAsync(cancellationToken);
    }

    public async Task<TSource> AddAsync(TSource entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<TSource>().Add(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task DeleteAsync(TSource entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<TSource>().Remove(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TSource entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<TSource>().Update(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
