using CatalogService.Domain.Contracts.Entities;

namespace CatalogService.Domain.Entities;

public class Category : Entity<Guid>
{
    public required string Name { get; set; }

    public Uri? Image { get; set; }

    public Category? ParentCategory { get; set; }
}
