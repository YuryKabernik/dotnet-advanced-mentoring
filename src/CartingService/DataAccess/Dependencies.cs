using CartingService.Abstractions.Interfaces;
using CartingService.DataAccess.Abstractions;
using CartingService.DataAccess.Interfaces;
using CartingService.DataAccess.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CartingService.DataAccess;

public static class Dependencies
{
    public static IServiceCollection Register(IServiceCollection services, IConfigurationRoot configuration)
    {
        IConfiguration section = configuration.GetSection(RepositorySettings.MongoSectionName);

        services.Configure<RepositorySettings>(section);
        
        services.AddScoped(typeof(MongoContext<>), typeof(IMongoContext<>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}