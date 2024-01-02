using Ardalis.Specification;
using CatalogService.Domain.Entities;

namespace CatalogService.Domain.Specs;

public sealed class ItemSpec : Specification<Item>
{
    public ItemSpec()
    {
        this.Query
            .Include(i => i.Category)
            .OrderBy(i => i.Name);
    }
}