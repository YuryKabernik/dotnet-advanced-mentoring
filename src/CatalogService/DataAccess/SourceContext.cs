using CatalogService.Domain.Entities;
using CatalogService.DataAccess.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CatalogService.DataAccess;

public class SourceContext : DbContext
{
    private readonly DatabaseSettings _settings;

    public SourceContext(IOptions<DatabaseSettings> settings)
    {
        this._settings = settings.Value;
    }

    public required DbSet<Category> Categories { get; set; }

    public required DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(
            this._settings.ConnectionString,
            options => options.CommandTimeout(this._settings.Timeout)
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SourceContext).Assembly);
    }
}