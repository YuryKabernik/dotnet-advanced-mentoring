using Ardalis.Specification;
using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Specs;

public sealed class ItemQuerySpec : Specification<Item>
{
    /// <summary>
    /// Get all Items including their categories.
    /// </summary>
    public ItemQuerySpec()
    {
        this.Query
            .Include(i => i.Category)
            .OrderBy(i => i.Name);
    }
    
    /// <summary>
    /// Get an Item by guid. 
    /// </summary>
    /// <param name="id"></param>
    public ItemQuerySpec(Guid id)
    {
        this.Query
            .Where(i => i.Id == id)
            .Include(i => i.Category)
            .OrderBy(i => i.Name);
    }
}
