using Microsoft.Extensions.DependencyInjection;

namespace StudentEnrollment.Api.Utils
{
    public static class DomainServiceRegistration
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            //services.AddScoped<IConfigurationService, ConfigurationService>();
        }
    }
}