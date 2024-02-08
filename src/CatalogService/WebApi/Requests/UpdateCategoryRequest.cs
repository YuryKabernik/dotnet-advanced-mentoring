namespace CatalogService.WebApi.Requests;

public class UpdateCategoryRequest
{
    public string? Name { get; set; }

    public string? Image { get; set; }

    public Guid? ParentCategory { get; set; }
}
