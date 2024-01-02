namespace CatalogService.WebApi.Requests;

public class UpdateCategoryRequest
{
    public string? Name { get; set; }
    public Uri? Image { get; set; }
    public int? ParentCategory { get; set; }
}
