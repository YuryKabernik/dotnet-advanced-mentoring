using CatalogService.DataAccess.SqlMappers;
using Dapper;

namespace CatalogService.DataAccess;

public class SqlMapperRegistry
{
    public static void Register()
    {
        SqlMapper.AddTypeHandler(new UriTypeMapper());
    }
}
