using CatalogService.BusinessLogic.Entities;
using CatalogService.Contracts.Interfaces;

namespace CatalogService.BusinessLogic.Managers;

public class ItemManager : BaseSourceManager<Item>
{
    public ItemManager(IRepository<Item> repository) : base(repository) { }

    public override async Task<Item?> GetAsync(string id) => await this.repository.GetFirst(i => i.Id == id);

    public override async Task<IEnumerable<Item>> GetAsync() => await this.repository.Get(_ => true, 100);

    public override async Task AddAsync(Item entity) => await this.repository.Add(entity);

    public override async Task DeleteAsync(string id)
    {
        var item = await repository.GetFirst(i => i.Id == id);

        if (item is not null)
        {
            await this.repository.Delete(item);
        }
    }

    public override async Task UpdateAsync(Item newEntity) => await this.repository.Update(newEntity);
}
