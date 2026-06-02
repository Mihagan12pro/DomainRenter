using DataAccess.Abstractions.Domains;
using DataAccess.Abstractions.Receipts;
using DataAccess.SQLite.DbContexts;
using DataAccess.SQLite.Domains;
using DataAccess.SQLite.Receipts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Domains;
using Services.Receipts;

namespace IntegrationTests
{
    public class DomainRenterAppFactory<TEntryPoint> :
        WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => 
            {
                services.AddDbContext<AppDbContextBase, TestDbContext>();

                services.AddScoped<IDomainsRepository, SQLiteDomainsRepository>();
                services.AddScoped<IReceiptsRepository, SQLiteReceiptsRepository>();

                services.AddScoped<IDomainsService, DomainsService>();
                services.AddScoped<IReceiptsService, ReceiptsService>();
            });
        }
    }
}
