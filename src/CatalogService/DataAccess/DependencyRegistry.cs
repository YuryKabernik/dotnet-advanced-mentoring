using CatalogService.DataAccess.Configuration;
using CatalogService.DataAccess.Repositories;
using CatalogService.Domain.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.DataAccess
{
    public static class DependencyRegistry
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var config = configuration.GetSection(DatabaseSettings.Key);
            services.Configure<DatabaseSettings>(config);

            services.AddDbContext<DbContext, SourceContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
