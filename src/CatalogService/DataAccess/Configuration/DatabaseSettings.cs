﻿namespace CatalogService.DataAccess.Configuration;

public class DatabaseSettings
{
    public static readonly string Key = "Catalog";

    public required string ConnectionString { get; set; }
    public required int Timeout { get; set; }
}
