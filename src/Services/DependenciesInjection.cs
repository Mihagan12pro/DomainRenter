using DataAccess.SQLite;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Services.Domains;

namespace Services
{
    public static class DependenciesInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(DependenciesInjection).Assembly);

            services.AddScoped<IDomainsService, DomainsService>();
            services.AddSQLiteServices();

            return services;
        }
    }
}
