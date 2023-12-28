using CatalogService.BusinessLogic.Entities;
using CatalogService.Domain.Contracts.Interfaces;

namespace CatalogService.BusinessLogic.Managers;

public class CategoryManager : BaseSourceManager<Category>
{
    public CategoryManager(IRepository<Category> repository) : base(repository) { }

    public override async Task<Category?> GetAsync(string id)
    {
        return await this.repository.GetFirst(c => c.Id == id);
    }

    public override async Task<IEnumerable<Category>> GetAsync()
    {
        return await this.repository.Get(_ => true, 100);
    }

    public override async Task AddAsync(Category entity)
    {
        await this.repository.Add(entity);
    }

    public override async Task DeleteAsync(string id)
    {
        var deleteCandidate = await this.repository.GetFirst(c => c.Id == id);

        if (deleteCandidate is not null)
        {
            await this.repository.Delete(deleteCandidate);
        }
    }

    public override async Task UpdateAsync(Category category)
    {
        var updateCandidtate = await this.repository.GetFirst(c => c.Id == category.Id);

        if (updateCandidtate is not null)
        {
            await this.repository.Update(category);
        }
    }
}
