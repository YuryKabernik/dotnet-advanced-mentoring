namespace CatalogService.BusinessLogic.Entities;

public class Category
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public Uri? Image { get; set; }
    public int? Parent { get; set; }
}
