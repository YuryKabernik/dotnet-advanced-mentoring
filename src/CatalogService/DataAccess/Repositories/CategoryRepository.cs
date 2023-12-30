using CatalogService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DataAccess.Repositories;

internal class CategoryRepository : BaseRepository<Category, Guid>
{
    public CategoryRepository(SourceContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await this.Context.Categories
            .Include(category => category.ParentCategory)
            .ToListAsync();
    }

    public override Task<Category?> GetSingleAsync(Guid guid)
    {
        return this.Context.Categories
            .Include(category => category.ParentCategory)
            .SingleOrDefaultAsync(c => c.Id == guid);
    }
}
