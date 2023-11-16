using CatalogService.BusinessLogic.Entities;
using LinqToDB;
using LinqToDB.Mapping;

namespace CatalogService.DataAccess;

public static class CategoryMapper
{
    public static void Map(FluentMappingBuilder builder)
    {
        var entityBuilder = builder.Entity<Category>()
            .HasTableName("Categories")
            .HasIdentity(x => x.Id)
            .HasPrimaryKey(x => x.Id);

        BuildNameColumn(entityBuilder);
        BuildImageColumn(entityBuilder);
        BuildParetColumn(entityBuilder);
    }

    private static void BuildParetColumn(EntityMappingBuilder<Category> entityBuilder)
    {
        entityBuilder.Member(x => x.Parent)
            .IsColumn()
            .IsNullable();
    }

    private static void BuildImageColumn(EntityMappingBuilder<Category> entityBuilder)
    {
        entityBuilder.Member(x => x.Image)
            .HasDataType(DataType.VarChar)
            .IsColumn()
            .IsNullable();
    }

    private static void BuildNameColumn(EntityMappingBuilder<Category> entityBuilder)
    {
        entityBuilder.Member(x => x.Name)
            .HasDataType(DataType.VarChar)
            .IsColumn()
            .IsNotNull()
            .HasLength(50);
    }
}
