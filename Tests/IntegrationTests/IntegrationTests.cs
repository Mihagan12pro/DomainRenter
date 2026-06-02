using DataAccess.Abstractions.Domains;
using DataAccess.Abstractions.Receipts;
using DataAccess.SQLite;
using DataAccess.SQLite.Domains;
using DataAccess.SQLite.Receipts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Domains;
using Services.Hosted;
using Services.Receipts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public abstract class IntegrationTests
    {
        protected async Task ResetDatabaseAsync()
        {
            var provider = await GetServiceProviderAsync();

            await using var context = provider.GetRequiredService<TestsDbContext>();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            await context.Database.MigrateAsync();
        }

        protected async Task<IServiceProvider> GetServiceProviderAsync()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddScoped<IReceiptsService, ReceiptsService>();
            services.AddScoped<IDomainsService, DomainsService>();

            services.AddDbContext<AppDbContext, TestsDbContext>();

            services.AddScoped<IDomainsRepository, SQLiteDomainsRepository>();
            services.AddScoped<IReceiptsRepository, SQLiteReceiptsRepository>();

            services.AddHostedService<DomainExpiryCheckService>();

            return services.BuildServiceProvider();
        }
    }
}
