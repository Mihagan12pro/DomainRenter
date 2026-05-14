using DataAccess.SQLite;
using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public static class DependenciesInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSQLiteServices();

            return services;
        }
    }
}
