using CatalogService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DataAccess.Repositories;

public class ItemRepository : BaseRepository<Item, Guid>
{
    public ItemRepository(SourceContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Item>> GetAllAsync()
    {
        return await this.Context.Items
            .Include(item => item.Category)
            .ToListAsync();
    }

    public override Task<Item?> GetSingleAsync(Guid guid)
    {
        return this.Context.Items
            .Include(item => item.Category)
            .SingleOrDefaultAsync(c => c.Id == guid);
    }
}