namespace CatalogService.WebApi.Requests;

public class AddCategoryRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Uri? Image { get; set; }
    public int Category { get; set; }
    public decimal Price { get; set; }
    public uint Amount { get; set; }
}
