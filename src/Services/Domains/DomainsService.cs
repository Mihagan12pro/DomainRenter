using Contracts.Domains;
using CSharpFunctionalExtensions;
using DataAccess.Abstractions.Domains;
using DomainModels.Domains;
using FluentValidation;
using Utils.Errors;
using Utils.Success;

namespace Services.Domains
{
    internal class DomainsService : IDomainsService
    {
        private readonly IDomainsRepository _domainsRepository;
        private readonly IValidator<RentDomainDto> _rentDomainValidator;

        private readonly SemaphoreSlim _semaphore
            = new SemaphoreSlim(1, 1);

        public async Task<Result<Success<string>, ErrorsCollection>> RentDomainAsync(
            RentDomainDto rentDomainDto, 
            CancellationToken cancellationToken)
        {
            var validationResult = await _rentDomainValidator.ValidateAsync(rentDomainDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ErrorsCollection(400, validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
            }

            string message = "The domain had been rented!";
            Result<Success<string>, ErrorsCollection> result;

            var domainModel = await _domainsRepository.GetByNameAsync(rentDomainDto.DomainName, cancellationToken);
            if (domainModel == null)
            {
                try
                {
                    await _semaphore.WaitAsync();

                    Guid id = await _domainsRepository.AddAsync(rentDomainDto.DomainName, cancellationToken);
                    await _domainsRepository.RentAsync(id, rentDomainDto.EndRentDate, cancellationToken);

                    result = new Success<string>(message);
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            else
            {
                if (!await _domainsRepository.IsRentedAsync(domainModel.Id, cancellationToken))
                {
                    try
                    {
                        await _semaphore.WaitAsync();

                        await _domainsRepository.RentAsync(domainModel.Id, rentDomainDto.EndRentDate, cancellationToken);

                        result = new Success<string>(message);
                    }
                    finally
                    {
                        _semaphore.Release();
                    }
                }
                else
                {
                    result = new ErrorsCollection(409, "This domain had been already rented!");
                }
            }

            return result;
        }

        public async Task<Result<Success<GetDomainDto>, ErrorsCollection>> GetByNameAsync(
            string name, 
            CancellationToken cancellationToken)
        {
            DomainModel domain = await _domainsRepository.GetByNameAsync(name, cancellationToken);

            if (domain == null)
                return new ErrorsCollection(404, $"This domain does not exists!");

            bool rented = await _domainsRepository.IsRentedAsync(domain.Id, cancellationToken);

            return new Success<GetDomainDto>(new GetDomainDto(domain.Name, rented));
        }

        public DomainsService(
            IDomainsRepository domainsRepository,
            IValidator<RentDomainDto> rentDomainValidator)
        {
            _domainsRepository = domainsRepository;
            _rentDomainValidator = rentDomainValidator;
        }
    }
}
