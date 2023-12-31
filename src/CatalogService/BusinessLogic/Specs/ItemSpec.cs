using Ardalis.Specification;
using CatalogService.BusinessLogic.Entities;

namespace CatalogService.BusinessLogic.Specs;

public sealed class ItemSpec : Specification<Item>
{
    public ItemSpec()
    {
        this.Query
            .Include(i => i.Category)
            .OrderBy(i => i.Name);
    }
}