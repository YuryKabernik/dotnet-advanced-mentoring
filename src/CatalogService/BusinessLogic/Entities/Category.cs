using CatalogService.Domain.Contracts.Entities;

namespace CatalogService.BusinessLogic.Entities;

public class Category : Entity<string>
{
    public required string Name { get; set; }
    public Uri? Image { get; set; }
    public Category? ParentCategory { get; set; }
}
