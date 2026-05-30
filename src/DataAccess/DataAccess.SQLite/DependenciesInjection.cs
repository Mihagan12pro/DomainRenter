using DataAccess.Abstractions.Domains;
using DataAccess.SQLite.Domains;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SQLite
{
    public static class DependenciesInjection
    {
        public static IServiceCollection AddSQLiteServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();

            services.AddScoped<IDomainsRepository, SQLiteDomainsRepository>();

            return services;
        }
    }
}
