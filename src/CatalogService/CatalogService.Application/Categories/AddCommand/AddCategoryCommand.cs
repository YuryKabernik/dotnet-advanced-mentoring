using MediatR;

namespace CatalogService.Application.Categories.AddCommand;

public class AddCategoryCommand : IRequest<AddCategoryCommandResponse>
{
    /// <summary>
    /// Category name is required for a new category.
    /// </summary>
    public required string Name { get; set; }

    public string? Image { get; set; }
    
    public Guid? ParentCategory { get; set; }
}
