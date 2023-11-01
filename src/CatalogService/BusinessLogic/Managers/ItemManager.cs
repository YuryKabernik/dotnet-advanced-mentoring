using CatalogService.BusinessLogic.Entities;
using CatalogService.Contracts;
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

    public Task DeleteAsync(int id) => this.repository.Delete(id);

    public Task<Item?> GetAsync(int id) => this.repository.Get(id);

    public Task<IEnumerable<Item>> ListAsync() => this.repository.List();

    public Task UpdateAsync(int id, Item newEntity) => this.repository.Update(id, newEntity);
}
