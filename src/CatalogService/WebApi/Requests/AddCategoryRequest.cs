namespace CatalogService.WebApi.Requests;

public class AddCategoryRequest
{
    public required string Name { get; set; }

    public string? Image { get; set; }

    public Guid? ParentCategory { get; set; }
}
