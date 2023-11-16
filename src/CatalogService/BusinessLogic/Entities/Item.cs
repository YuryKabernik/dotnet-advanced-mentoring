namespace CatalogService.BusinessLogic.Entities;

public class Item
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Uri? Image { get; set; }
    public required int Category { get; set; }
    public required decimal Price { get; set; }
    public required uint Amount { get; set; }
}
