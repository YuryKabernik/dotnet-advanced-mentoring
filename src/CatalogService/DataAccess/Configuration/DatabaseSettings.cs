using LinqToDB.Mapping;

namespace CatalogService.DataAccess.Configuration;

public class DatabaseSettings
{
    public required string ConnectionString { get; set; }
    public required int Timeout { get; set; }
    public required string Provider { get; set; }
    public required MappingSchema MappingSchema { get; set; }
}
