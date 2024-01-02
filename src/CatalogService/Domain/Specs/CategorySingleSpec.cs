using Ardalis.Specification;
using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Specs;

public sealed class CategorySingleSpec : SingleResultSpecification<Category>
{
    public CategorySingleSpec(Guid id)
    {
        this.Query
            .Where(c => c.Id == id)
            .Include(c => c.ParentCategory);
    }
}