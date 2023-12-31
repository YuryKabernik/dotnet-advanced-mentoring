using Ardalis.Specification;
using CatalogService.BusinessLogic.Entities;

namespace CatalogService.BusinessLogic.Specs;

public sealed class CategorySpec : Specification<Category>
{
    /// <summary>
    /// Get all Categories including their parent categories.
    /// </summary>
    public CategorySpec()
    {
        this.Query
            .Include(c => c.ParentCategory)
            .OrderBy(c => c.Name);
    }
}