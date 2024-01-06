using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.DataAccess.EntityMappers;

public class CategoryMapper : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .ToTable("Categories")
            .HasKey(x => x.Id);

        BuildNameColumn(builder);
        BuildImageColumn(builder);
        BuildParentCategoryColumn(builder);

        BuildItemsRelationship(builder);
    }

    private static void BuildParentCategoryColumn(EntityTypeBuilder<Category> entityBuilder)
    {
        entityBuilder
            .Navigation(x => x.ParentCategory)
            .IsRequired(false);
    }

    private static void BuildImageColumn(EntityTypeBuilder<Category> entityBuilder)
    {
        entityBuilder
            .Property(x => x.Image)
            .IsRequired(false);
    }

    private static void BuildNameColumn(EntityTypeBuilder<Category> entityBuilder)
    {
        entityBuilder
            .Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();
    }
    
    private static void BuildItemsRelationship(EntityTypeBuilder<Category> builder)
    {
        builder
            .Navigation(x => x.Items)
            .IsRequired();
    }
}