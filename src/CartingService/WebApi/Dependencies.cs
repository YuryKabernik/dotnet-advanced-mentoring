using CartingService.BusinessLogic.Interfaces.Services;
using CartingService.WebApi.Options;
using CartingService.WebApi.Services;
using BusinessLogicDependencies = CartingService.BusinessLogic.Dependencies;

namespace CartingService.WebApi;

/// <summary>
/// Dependencies registrar for the WebApi application.
/// </summary>
public static class Dependencies
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection Register(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services
            .AddHttpClient()
            .AddCatalogService(configuration);

        BusinessLogicDependencies.Register(services, configuration);

        return services;
    }

    private static void AddCatalogService(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var config = configuration.GetSection(CatalogServiceOptions.Key);
        
        services.Configure<CatalogServiceOptions>(config);
        services.AddScoped<ICatalogService, CatalogService>();
    }
}
