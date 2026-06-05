using DataAccess.Abstractions.Domains;
using DataAccess.Abstractions.Receipts;
using DataAccess.SQLite.DbContexts;
using DataAccess.SQLite.Domains;
using DataAccess.SQLite.Receipts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Services.Domains;
using Services.Receipts;

namespace IntegrationTests
{
    public abstract class IntegrationTests
        : IClassFixture<DomainRenterAppFactory<Program>>,
        IAsyncLifetime
    {
        private string _dataSource = $"Data Source={Guid.NewGuid()}.db";

        protected readonly DomainRenterAppFactory<Program> factory;
        protected readonly HttpClient httpClient;

        public IntegrationTests(DomainRenterAppFactory<Program> factory)
        {
            this.factory = factory;

            httpClient = factory.CreateClient();
        }

        public async Task DisposeAsync()
        {
            var provider = BuildProvider();

            var context = provider.GetRequiredService<AppDbContextBase>();

            context.Database.EnsureDeleted();
        }

        public async Task InitializeAsync()
        {
            var provider = BuildProvider();

            var context = provider.GetRequiredService<AppDbContextBase>();  

            context.Database.EnsureCreated();
        }

        protected IServiceProvider BuildProvider()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddDbContext<AppDbContextBase, TestDbContext>(options =>
            {
                options.UseSqlite(_dataSource);
            });

            services.AddScoped<IDomainsRepository, SQLiteDomainsRepository>();
            services.AddScoped<IReceiptsRepository, SQLiteReceiptsRepository>();

            services.AddScoped<IDomainsService, DomainsService>();
            services.AddScoped<IReceiptsService, ReceiptsService>();

            return services.BuildServiceProvider();
        }
    }
}
