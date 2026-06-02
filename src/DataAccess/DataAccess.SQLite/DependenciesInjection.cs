using DataAccess.Abstractions.Domains;
using DataAccess.Abstractions.Receipts;
using DataAccess.SQLite.Domains;
using DataAccess.SQLite.Receipts;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("IntegrationTests")]
namespace DataAccess.SQLite
{
    public static class DependenciesInjection
    {
        public static IServiceCollection AddSQLiteServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();

            services.AddScoped<IDomainsRepository, SQLiteDomainsRepository>();
            services.AddScoped<IReceiptsRepository, SQLiteReceiptsRepository>();

            return services;
        }
    }
}
