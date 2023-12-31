using Ardalis.Specification;
using CatalogService.BusinessLogic.Entities;

namespace CatalogService.BusinessLogic.Specs;

public sealed class ItemSingleSpec : SingleResultSpecification<Item>
{
    public ItemSingleSpec()
    {
        this.Query
            .Include(i => i.Category)
            .OrderBy(i => i.Name);
    }
}