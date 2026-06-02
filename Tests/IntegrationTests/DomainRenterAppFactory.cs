using DataAccess.Abstractions.Domains;
using DataAccess.Abstractions.Receipts;
using DataAccess.SQLite;
using DataAccess.SQLite.Domains;
using DataAccess.SQLite.Receipts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Services.Domains;
using Services.Hosted;
using Services.Receipts;

namespace IntegrationTests
{
    public class DomainRenterAppFactory<TEntryPoint>
        : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IReceiptsService, ReceiptsService>();
                services.AddScoped<IDomainsService, DomainsService>();

                services.AddDbContext<AppDbContext, TestsDbContext>();

                services.AddScoped<IDomainsRepository, SQLiteDomainsRepository>();
                services.AddScoped<IReceiptsRepository, SQLiteReceiptsRepository>();

                services.AddHostedService<DomainExpiryCheckService>();
            });

            builder.UseEnvironment("Development");
        }
    }
}
