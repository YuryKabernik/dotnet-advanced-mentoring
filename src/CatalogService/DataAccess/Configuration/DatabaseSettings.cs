namespace CatalogService.DataAccess.Configuration;

public class DatabaseSettings
{
    public required string ConnectionString { get; set; }
    public required int Timeout { get; set; }
}
