using MediatR;

namespace CatalogService.Application.Categories.UpdateCommand;

public record UpdateCategoryCommand(Guid CategoryId, CategoryUpdateValues Details) : IRequest<UpdateCategoryCommandResponse>;

public class CategoryUpdateValues
{
    /// <summary>
    /// Optionally. Update the name of the category.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Optionally. Update image url.
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// Optionally. Update the parent category.
    /// </summary>
    public Guid? ParentCategory { get; set; }
}
