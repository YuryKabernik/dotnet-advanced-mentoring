using Microsoft.Extensions.DependencyInjection;

namespace CartingService.BusinessLogic
{
    public class Dependencies
    {
        public static IServiceCollection Register(IServiceCollection services)
        {
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblyContaining<Dependencies>());

            return services;
        }
    }
}
