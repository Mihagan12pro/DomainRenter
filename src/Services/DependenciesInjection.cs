using DataAccess.SQLite;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Services.Domains;
using Services.Hosted;
using Services.Receipts;

namespace Services
{
    public static class DependenciesInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(DependenciesInjection).Assembly);

            services.AddHostedService<DomainExpiryCheckService>();

            services.AddScoped<IDomainsService, DomainsService>();
            services.AddScoped<IReceiptsService, ReceiptsService>();

            services.AddSQLiteServices();

            return services;
        }
    }
}
