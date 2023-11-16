using CatalogService.BusinessLogic.Entities;
using LinqToDB;
using LinqToDB.Mapping;

namespace CatalogService.DataAccess;

public static class ItemsMapper
{
    public static void Map(FluentMappingBuilder builder)
    {
        var entityBuilder = builder.Entity<Item>()
            .HasTableName("Items")
            .HasIdentity(x => x.Id)
            .HasPrimaryKey(x => x.Id);

        BuildNameColumn(entityBuilder);
        BuildDescriptionColumn(entityBuilder);
        BuildImageColumn(entityBuilder);
        BuildCategoryColumn(entityBuilder);
        BuildPriceColumn(entityBuilder);
        BuildAmountColumn(entityBuilder);
    }

    private static void BuildAmountColumn(EntityMappingBuilder<Item> entity)
    {
        entity.Member(x => x.Amount)
            .IsColumn()
            .HasDataType(DataType.UInt32)
            .IsNotNull();
    }

    private static void BuildPriceColumn(EntityMappingBuilder<Item> entity)
    {
        entity.Member(x => x.Price)
            .IsColumn()
            .HasDataType(DataType.Money)
            .IsNotNull();
    }

    private static void BuildCategoryColumn(EntityMappingBuilder<Item> entity)
    {
        entity.Member(x => x.Category)
            .IsColumn()
            .IsNotNull();
    }

    private static void BuildImageColumn(EntityMappingBuilder<Item> entity)
    {
        entity.Member(x => x.Image)
            .IsColumn()
            .HasDataType(DataType.VarChar)
            .IsNullable();
    }

    private static void BuildDescriptionColumn(EntityMappingBuilder<Item> entity)
    {
        entity.Member(x => x.Description)
            .IsColumn()
            .HasDataType(DataType.NText)
            .IsNullable();
    }

    private static void BuildNameColumn(EntityMappingBuilder<Item> entity)
    {
        entity.Member(x => x.Name)
            .IsColumn()
            .IsNotNull()
            .HasDataType(DataType.VarChar)
            .HasLength(50);
    }
}
