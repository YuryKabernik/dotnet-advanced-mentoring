using CatalogService.BusinessLogic.Entities;
using CatalogService.Domain.Contracts.Interfaces;

namespace CatalogService.Application.Managers;

public class ItemManager : BaseSourceManager<Item>
{
    public ItemManager(IRepository<Item> repository) : base(repository) { }

    public override async Task<Item?> GetAsync(string id) => await this._repository.GetSingleAsync(i => i.Id == id);

    public override async Task<IEnumerable<Item>> GetAsync() => await this._repository.GetTopAsync(_ => true, 100);

    public override async Task AddAsync(Item entity) => await this._repository.AddAsync(entity);

    public override async Task DeleteAsync(string id)
    {
        var item = await _repository.GetSingleAsync(i => i.Id == id);

        if (item is not null)
        {
            await this._repository.DeleteAsync(item);
        }
    }

    public override async Task UpdateAsync(Item newEntity) => await this._repository.UpdateAsync(newEntity);
}
