using CatalogService.DataAccess.Configuration;
using LinqToDB;
using LinqToDB.Data;

namespace CatalogService.DataAccess;

public class DbContext<TTable> : DataConnection where TTable : class
{
    public DbContext(DatabaseSettings settings)
        : base(settings.Provider, settings.ConnectionString, settings.MappingSchema)
    {
        this.CommandTimeout = settings.Timeout;
    }

    public ITable<TTable> Table => this.GetTable<TTable>();
}
