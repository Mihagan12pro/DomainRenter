using DataAccess.SQLite;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Services.Domains;
using Services.Hosted;

namespace Services
{
    public static class DependenciesInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(DependenciesInjection).Assembly);

            services.AddHostedService<DomainExpiryCheckService>();

            services.AddScoped<IDomainsService, DomainsService>();
            services.AddSQLiteServices();

            return services;
        }
    }
}
