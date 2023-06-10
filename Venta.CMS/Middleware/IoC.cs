using Venta.Data;
using Venta.Services;

namespace Venta.CMS.Middleware
{
    public static class IoC
    {
        public static IServiceCollection AddApplicationDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddRepositories(configuration);
            services.AddServices();

            return services;
        }
    }
}
