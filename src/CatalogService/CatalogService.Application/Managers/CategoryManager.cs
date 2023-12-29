using CatalogService.BusinessLogic.Entities;
using CatalogService.Domain.Contracts.Interfaces;

namespace CatalogService.Application.Managers;

public class CategoryManager : BaseSourceManager<Category>
{
    public CategoryManager(IRepository<Category> repository) : base(repository) { }

    public override async Task<Category?> GetAsync(string id)
    {
        return await this._repository.GetSingleAsync(c => c.Id == id);
    }

    public override async Task<IEnumerable<Category>> GetAsync()
    {
        return await this._repository.GetTopAsync(_ => true, 100);
    }

    public override async Task AddAsync(Category entity)
    {
        await this._repository.AddAsync(entity);
    }

    public override async Task DeleteAsync(string id)
    {
        var deleteCandidate = await this._repository.GetSingleAsync(c => c.Id == id);

        if (deleteCandidate is not null)
        {
            await this._repository.DeleteAsync(deleteCandidate);
        }
    }

    public override async Task UpdateAsync(Category category)
    {
        var updateCandidate = await this._repository.GetSingleAsync(c => c.Id == category.Id);

        if (updateCandidate is not null)
        {
            await this._repository.UpdateAsync(category);
        }
    }
}
