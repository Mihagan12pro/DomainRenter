using Contracts.Domains;
using DataAccess.Abstractions.Domains;
using DomainModels.Domains;
using System.Linq.Expressions;

namespace Services.Domains
{
    internal class DomainsService : IDomainsService
    {
        private readonly IDomainsRepository _domainsRepository;

        private readonly SemaphoreSlim _semaphore
            = new SemaphoreSlim(1, 1);

        public Task AddDomainAsync(
            DomainModel domain, 
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task GetDomainsAsync(
            Expression<Func<bool, DomainModel>> filters,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> RentDomainAsync(
            RentDomainDto rentDomainDto, 
            CancellationToken cancellationToken)
        {
            var domainModel = await _domainsRepository.GetByNameAsync(rentDomainDto.DomainName, cancellationToken);
            if (domainModel == null)
            {
                try
                {
                    await _semaphore.WaitAsync();

                    await _domainsRepository.AddAsync(rentDomainDto.DomainName, cancellationToken);


                }
                finally
                {
                    _semaphore.Release();
                }
            }
            else
            {

            }

                throw new NotImplementedException();
        }

        public DomainsService(IDomainsRepository domainsRepository)
        {
            _domainsRepository = domainsRepository;
        }
    }
}
