using DataAccess.SQLite;
using Microsoft.Extensions.DependencyInjection;
using Services.Domains;

namespace Services
{
    public static class DependenciesInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDomainsService, DomainsService>();

            services.AddSQLiteServices();

            return services;
        }
    }
}
