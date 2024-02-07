using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAccessDependencies = CartingService.DataAccess.Dependencies;

namespace CartingService.BusinessLogic
{
    public class Dependencies
    {
        public static IServiceCollection Register(IServiceCollection services, IConfigurationRoot configuration)
        {
            DataAccessDependencies.Register(services, configuration);

            services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Dependencies>());

            return services;
        }
    }
}
