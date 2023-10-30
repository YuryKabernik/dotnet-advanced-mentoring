namespace CartingService.DataAccess.Settings;

public class RepositorySettings
{
    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
}