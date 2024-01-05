using CatalogService.Application.Abstractions;
using CatalogService.Domain.Contracts.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Specs;

namespace CatalogService.Application.Categories.AddCommand;

public class AddCategoryCommandHandler : ICommandHandler<AddCategoryCommand, AddCategoryCommandResponse>
{
    private readonly IRepository<Category> _categoryRepository;

    public AddCategoryCommandHandler(IRepository<Category> categoryRepository)
    {
        this._categoryRepository = categoryRepository;
    }

    public async Task<AddCategoryCommandResponse> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var newCategory = await this.CreateCategory(request);
        var category = await this._categoryRepository.AddAsync(newCategory, cancellationToken);

        return new AddCategoryCommandResponse(category);
    }

    private async Task<Category> CreateCategory(AddCategoryCommand command)
    {
        ArgumentException.ThrowIfNullOrEmpty(command.Name, "Category Name can't be null or empty");
        ArgumentException.ThrowIfNullOrEmpty(command.Image, "Image URL can't be null or empty.");

        Category category = new()
        {
            Id = Guid.Empty,
            Name = command.Name,
            Image = new Uri(command.Image)
        };

        if (command.ParentCategory is not null)
        {
            var query = new CategoryQuerySpec(command.ParentCategory.Value);
            var parentCategory = await this._categoryRepository.GetAsync(query);

            category.ParentCategory = parentCategory;
        }

        return category;
    }
}
