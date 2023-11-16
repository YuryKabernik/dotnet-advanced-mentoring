using CatalogService.BusinessLogic.Entities;
using CatalogService.Contracts.Interfaces;

namespace CatalogService.BusinessLogic.Managers;

public class ItemManager : IManager<Item>
{
    private readonly IRepository<Item> repository;

    public ItemManager(IRepository<Item> repository)
    {
        this.repository = repository;
    }
    public Task AddAsync(Item entity) => this.repository.Add(entity);

    public Task DeleteAsync(string id) => this.repository.Delete(id);

    public Task<Item?> GetAsync(string id) => this.repository.Get(id);

    public Task<IEnumerable<Item>> GetAsync() => this.repository.Get();

    public Task UpdateAsync(string id, Item newEntity) => this.repository.Update(id, newEntity);
}
