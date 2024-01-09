using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public class DependencyRegistry
    {
        public static IServiceCollection AddDependencies(IServiceCollection services)
        {
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<DependencyRegistry>());

            return services;
        }
    }
}
