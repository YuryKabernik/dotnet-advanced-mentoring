using CatalogService.BusinessLogic.Entities;
using CatalogService.Contracts.Interfaces;

namespace CatalogService.BusinessLogic.Managers;

public class CategoryManager : IManager<Category>
{
    private readonly IRepository<Category> repository;

    public CategoryManager(IRepository<Category> repository)
    {
        this.repository = repository;
    }

    public Task AddAsync(Category entity) => this.repository.Add(entity);

    public Task DeleteAsync(string id) => this.repository.Delete(id);

    public Task<Category?> GetAsync(string id) => this.repository.Get(id);

    public Task<IEnumerable<Category>> GetAsync() => this.repository.Get();

    public Task UpdateAsync(string id, Category newEntity) => this.repository.Update(id, newEntity);
}
