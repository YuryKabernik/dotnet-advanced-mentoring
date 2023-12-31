using Ardalis.Specification;
using CatalogService.BusinessLogic.Entities;

namespace CatalogService.BusinessLogic.Specs;

public sealed class ItemSingleSpec : SingleResultSpecification<Item>
{
    public ItemSingleSpec(Guid id)
    {
        this.Query
            .Where(i => i.Id == id)
            .Include(i => i.Category)
            .OrderBy(i => i.Name);
    }
}