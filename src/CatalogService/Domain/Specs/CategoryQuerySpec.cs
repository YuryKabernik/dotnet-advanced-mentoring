using Ardalis.Specification;
using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Specs;

public sealed class CategoryQuerySpec : Specification<Category>
{
    /// <summary>
    /// Get all Categories including their parent categories.
    /// </summary>
    public CategoryQuerySpec() =>
        this.Query
            .BuildBasic();

    /// <summary>
    /// Get a Category by guid. 
    /// </summary>
    /// <param name="id"></param>
    public CategoryQuerySpec(Guid id) =>
        this.Query
            .BuildBasic()
            .Where(c => c.Id == id);
}

internal static class CategorySpecExtentions
{
    public static ISpecificationBuilder<Category> BuildBasic(this ISpecificationBuilder<Category> query)
    {
        return query
            .Include(c => c.ParentCategory)
            .OrderBy(c => c.Name);
    }
}
