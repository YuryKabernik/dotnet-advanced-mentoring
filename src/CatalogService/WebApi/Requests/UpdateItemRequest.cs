namespace CatalogService.WebApi.Endpoints;

public class UpdateItemRequest
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public Guid? Category { get; set; }

    public decimal? Price { get; set; }

    public int? Amount { get; set; }
}