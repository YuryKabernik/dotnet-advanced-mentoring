using CatalogService.Application.Abstractions;
using CatalogService.Domain.Contracts.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Specs;

namespace Application.Items.AddCommand;

public class AddItemCommandHandler : ICommandHandler<AddItemCommand, AddItemCommandResponse>
{
    private readonly IRepository<Item> _itemsRepository;
    private readonly IRepository<Category> _categoryRepository;

    public AddItemCommandHandler(
        IRepository<Item> itemsRepository,
        IRepository<Category> categoryRepository)
    {
        _itemsRepository = itemsRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<AddItemCommandResponse> Handle(
        AddItemCommand command,
        CancellationToken cancellationToken)
    {
        var newItem = await CreateNewItem(command, cancellationToken);
        var item = await this._itemsRepository.AddAsync(newItem, cancellationToken);

        return new AddItemCommandResponse(item);
    }

    private async Task<Item> CreateNewItem(AddItemCommand command, CancellationToken cancellationToken)
    {
        var query = new CategoryQuerySpec(command.Category);
        var category = await this._categoryRepository.GetAsync(query, cancellationToken);

        return new Item()
        {
            Id = Guid.Empty,
            Category = category,
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            Amount = command.Amount
        };
    }
}