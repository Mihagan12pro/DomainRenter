using DataAccess.Abstractions.Domains;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Services.Hosted
{
    internal class DomainExpiryCheckService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000);

                var now = DateOnly.FromDateTime(DateTime.Now);

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    IDomainsRepository domainsRepository = scope.ServiceProvider.GetRequiredService<IDomainsRepository>();

                    var expired = await domainsRepository.GetRentedAsync(
                        (rd => rd.EndOfRenting <= now),
                        stoppingToken);

                    foreach(var e in expired)
                    {
                        await domainsRepository.EndRentAsync(e.DomainId, stoppingToken);
                    }
                }
            }
        }

        public DomainExpiryCheckService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
    }
}
