using DataAccess.Abstractions.Domains;
using DomainModels.Domains;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq.Expressions;
using Utils.Pagination;

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
                        new List<Expression<Func<RentedDomainModel, bool>>> { (rd => rd.EndOfRenting <= now) },
                        new Pagination<RentedDomainModel>(),
                        stoppingToken);

                    foreach(var e in expired.Collection)
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
