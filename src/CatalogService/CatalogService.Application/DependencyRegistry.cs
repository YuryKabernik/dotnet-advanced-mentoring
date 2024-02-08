using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Application
{
    public static class DependencyRegistry
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining(typeof(DependencyRegistry)));

            return services;
        }
    }
}
