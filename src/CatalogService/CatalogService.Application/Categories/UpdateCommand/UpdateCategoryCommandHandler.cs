using CatalogService.Application.Abstractions;
using CatalogService.Domain.Contracts.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Specs;

namespace CatalogService.Application.Categories.UpdateCommand;

public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
{
    private readonly IRepository<Category> _categoryRepository;

    public UpdateCategoryCommandHandler(IRepository<Category> categoryRepository)
    {
        this._categoryRepository = categoryRepository;
    }

    public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var query = new CategoryQuerySpec(command.CategoryId);
        var targetCategory = await this._categoryRepository.GetAsync(query, cancellationToken);

        await this.UpdatePropertiesAsync(targetCategory, command);
        await this._categoryRepository.UpdateAsync(targetCategory, cancellationToken);

        return new UpdateCategoryCommandResponse(targetCategory);
    }

    private async Task UpdatePropertiesAsync(Category category, UpdateCategoryCommand command)
    {
        category.Name = string.IsNullOrWhiteSpace(command.Details.Name) ? category.Name : command.Details.Name;
        category.Image = string.IsNullOrWhiteSpace(command.Details.Image) ? category.Image : new Uri(command.Details.Image);

        if (command.Details.ParentCategory is not null)
        {
            var query = new CategoryQuerySpec(command.Details.ParentCategory.Value);
            var parentCategory = await this._categoryRepository.GetAsync(query);

            category.ParentCategory = parentCategory;
        }
    }
}
