using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.DataAccess.EntityMappers;

public class ItemsMapper : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder
            .ToTable("Items")
            .HasKey(x => x.Id);

        BuildNameColumn(builder);
        BuildDescriptionColumn(builder);
        BuildImageColumn(builder);
        BuildCategoryColumn(builder);
        BuildPriceColumn(builder);
        BuildAmountColumn(builder);
    }

    private static void BuildAmountColumn(EntityTypeBuilder<Item> entity)
    {
        entity.Property(x => x.Amount)
            .IsRequired();
    }

    private static void BuildPriceColumn(EntityTypeBuilder<Item> entity)
    {
        entity.Property(x => x.Price)
            .HasColumnType("money()")
            .IsRequired();
    }

    private static void BuildCategoryColumn(EntityTypeBuilder<Item> entity)
    {
        entity.Property(x => x.Category)
            .IsRequired();
    }

    private static void BuildImageColumn(EntityTypeBuilder<Item> entity)
    {
        entity.Property(x => x.Image)
            .HasConversion<string>()
            .IsRequired(false);
    }

    private static void BuildDescriptionColumn(EntityTypeBuilder<Item> entity)
    {
        entity.Property(x => x.Description)
            .HasMaxLength(10000)
            .IsRequired(false);
    }

    private static void BuildNameColumn(EntityTypeBuilder<Item> entity)
    {
        entity.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();
    }
}
