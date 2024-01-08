using CartingService.BusinessLogic.Interfaces.Services;
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
        services.AddHttpClient();
        services.AddScoped<ICatalogService, CatalogService>();

        BusinessLogicDependencies.Register(services, configuration);

        return services;
    }
}
