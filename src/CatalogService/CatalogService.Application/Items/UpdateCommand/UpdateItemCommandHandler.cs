using CatalogService.Application.Abstractions;
using CatalogService.Domain.Contracts.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Specs;

namespace Application.Items.UpdateCommand;

public class UpdateItemCommandHandler : ICommandHandler<UpdateItemCommand, UpdateItemCommandResponse>
{
    private readonly IRepository<Item> _itemsRepository;
    private readonly IRepository<Category> _categoryRepository;

    public UpdateItemCommandHandler(
        IRepository<Item> itemsRepository,
        IRepository<Category> categoryRepository)
    {
        _itemsRepository = itemsRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<UpdateItemCommandResponse> Handle(UpdateItemCommand command, CancellationToken cancellationToken)
    {
        var query = new ItemQuerySpec(command.Id);
        var item = await _itemsRepository.GetAsync(query, cancellationToken);

        await this.UpdatePropertiesAsync(item, command, cancellationToken);
        await this._itemsRepository.UpdateAsync(item, cancellationToken);

        return new UpdateItemCommandResponse(item);
    }

    private async Task UpdatePropertiesAsync(Item item, UpdateItemCommand command, CancellationToken cancellationToken)
    {
        item.Name = command.Values.Name ?? item.Name;
        item.Description = command.Values.Description ?? item.Description;

        item.Image = string.IsNullOrWhiteSpace(command.Values.Image) ? item.Image : new Uri(command.Values.Image!);

        item.Price = command.Values.Price ?? item.Price;
        item.Amount = command.Values.Amount ?? item.Amount;

        if (command.Values.Category is not null)
        {
            var query = new CategoryQuerySpec(command.Values.Category.Value);
            item.Category = await this._categoryRepository.GetAsync(query, cancellationToken);
        }
    }
}