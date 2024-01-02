using CatalogService.Domain.Entities;
using CatalogService.DataAccess.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DataAccess;

public class SourceContext : DbContext
{
    private readonly DatabaseSettings _settings;

    public SourceContext(DatabaseSettings settings)
    {
        _settings = settings;
    }

    public required DbSet<Category> Categories { get; set; }
    public required DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(
            _settings.ConnectionString,
            options => options.CommandTimeout(_settings.Timeout)
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SourceContext).Assembly);
    }
}