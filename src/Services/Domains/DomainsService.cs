using Contracts.Domains;
using DataAccess.Abstractions.Domains;
using DomainModels.Domains;
using HelpEntities;
using System.Linq.Expressions;

namespace Services.Domains
{
    internal class DomainsService : IDomainsService
    {
        private readonly IDomainsRepository _domainsRepository;

        private readonly SemaphoreSlim _semaphore
            = new SemaphoreSlim(1, 1);

        public Task GetDomainsAsync(
            Expression<Func<bool, DomainModel>> filters,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<string, string>> RentDomainAsync(
            RentDomainDto rentDomainDto, 
            CancellationToken cancellationToken)
        {
            var result = new Result<string, string>();
            string message = "The domain had been rented!";

            var domainModel = await _domainsRepository.GetByNameAsync(rentDomainDto.DomainName, cancellationToken);
            if (domainModel == null)
            {
                try
                {
                    await _semaphore.WaitAsync();

                    Guid id = await _domainsRepository.AddAsync(rentDomainDto.DomainName, cancellationToken);
                    await _domainsRepository.RentAsync(id, rentDomainDto.EndRentDate, cancellationToken);

                    result.Value = message;
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            else
            {
                if (domainModel.RentedDomain == null)
                {
                    try
                    {
                        await _semaphore.WaitAsync();

                        await _domainsRepository.RentAsync(domainModel.Id, rentDomainDto.EndRentDate, cancellationToken);

                        result.Value = message;
                    }
                    finally
                    {
                        _semaphore.Release();
                    }
                }
                else
                {
                    result.Fail = message;
                }
            }

            return result;
        }

        public DomainsService(IDomainsRepository domainsRepository)
        {
            _domainsRepository = domainsRepository;
        }
    }
}
