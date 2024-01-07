namespace CartingService.DataAccess.Settings;

public class RepositorySettings
{
    public static readonly string MongoSectionName = "Mongo";
    
    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
    public required string CollectionName { get; set; }
}
