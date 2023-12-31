using Ardalis.Specification;
using CatalogService.BusinessLogic.Entities;

namespace CatalogService.BusinessLogic.Specs;

public sealed class CategorySingleSpec : SingleResultSpecification<Category>
{
    public CategorySingleSpec(Guid id)
    {
        this.Query
            .Where(c => c.Id == id)
            .Include(c => c.ParentCategory);
    }
}