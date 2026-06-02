using DataAccess.Abstractions.Domains;
using DataAccess.Abstractions.Receipts;
using DataAccess.SQLite.DbContexts;
using DataAccess.SQLite.Domains;
using DataAccess.SQLite.Receipts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Domains;
using Services.Hosted;
using Services.Receipts;
using System.Xml.Linq;

namespace UnitTests
{
    public abstract class UnitTests
    {
        public IServiceProvider CreateProvider()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddValidatorsFromAssembly(typeof(Services.DependenciesInjection).Assembly);

            services.AddScoped<IReceiptsService, ReceiptsService>();
            services.AddScoped<IDomainsService, DomainsService>();

            services.AddDbContext<AppDbContextBase, InMemeoryAppDbContext>(options =>
                options.UseInMemoryDatabase("Data Source=test.db"));

            services.AddScoped<IDomainsRepository, SQLiteDomainsRepository>();
            services.AddScoped<IReceiptsRepository, SQLiteReceiptsRepository>();

            services.AddHostedService<DomainExpiryCheckService>();

            var provider = services.BuildServiceProvider();

            return provider;
        }
    }
}
