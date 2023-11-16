using LinqToDB.Mapping;

namespace CatalogService.DataAccess.Mappers.Interfaces;

public interface ITableSchemaMapper
{
    void Map(FluentMappingBuilder builder);
}
