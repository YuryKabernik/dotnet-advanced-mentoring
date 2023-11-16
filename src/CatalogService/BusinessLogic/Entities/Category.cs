using CatalogService.Contracts.Interfaces;

namespace CatalogService.BusinessLogic.Entities;

public class Category : ITableModel
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public Uri? Image { get; set; }
    public int? Parent { get; set; }
}
