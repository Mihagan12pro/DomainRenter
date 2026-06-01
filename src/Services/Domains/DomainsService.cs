using Contracts.Domains;
using CSharpFunctionalExtensions;
using DataAccess.Abstractions.Domains;
using DataAccess.Abstractions.Receipts;
using DomainModels.Domains;
using FluentValidation;
using System.Linq.Expressions;
using Utils.Errors;
using Utils.Pagination;
using Utils.Pagination.Collections;
using Utils.Success;

namespace Services.Domains
{
    internal class DomainsService : IDomainsService
    {
        private readonly IDomainsRepository _domainsRepository;
        private readonly IReceiptsRepository _receiptsRepository;

        private readonly IValidator<RentDomainDto> _rentDomainValidator;

        private readonly SemaphoreSlim _semaphore
            = new SemaphoreSlim(1, 1);

        public async Task<Result<Success<Guid>, ErrorsCollection>> RentDomainAsync(
            RentDomainDto rentDomainDto, 
            CancellationToken cancellationToken)
        {
            string domainName = rentDomainDto.DomainName.ToLowerInvariant();

            rentDomainDto = rentDomainDto with
            {
                DomainName = domainName
            };

            var validationResult = await _rentDomainValidator.ValidateAsync(rentDomainDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ErrorsCollection(400, validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
            }

            string message = "The domain had been rented!";
            Result<Success<Guid>, ErrorsCollection> result;

            var domainModel = await _domainsRepository.GetByNameAsync(rentDomainDto.DomainName, cancellationToken);
            if (domainModel == null)
            {
                try
                {
                    await _semaphore.WaitAsync();

                    Guid id = await _domainsRepository.AddAsync(rentDomainDto.DomainName, cancellationToken);
                    Guid domainId = await _domainsRepository.RentAsync(id, rentDomainDto.EndRentDate, cancellationToken);


                    Guid receiptId = await _receiptsRepository.AddAsync(domainId, rentDomainDto.User, cancellationToken);

                    result = new Success<Guid>(receiptId);
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

                        Guid domainId = await _domainsRepository.RentAsync(domainModel.Id, rentDomainDto.EndRentDate, cancellationToken);
                        Guid receiptId = await _receiptsRepository.AddAsync(domainId, rentDomainDto.User, cancellationToken);

                        result = new Success<Guid>(receiptId);
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

        public async Task<Result<Success, ErrorsCollection>> EndRentAsync(
            string name,
            CancellationToken cancellationToken)
        {
            DomainModel domain = await _domainsRepository.GetByNameAsync(name, cancellationToken);

            if (domain == null)
                return new ErrorsCollection(404, $"This domain does not exists!");

            if (!await _domainsRepository.IsRentedAsync(domain.Id, cancellationToken))
                return new ErrorsCollection(409, $"This domain does not rented!");

            await _domainsRepository.EndRentAsync(domain.Id, cancellationToken);

            return new Success();
        }

        public async Task<PaginatedCollection<string>> GetRentedDomainsAsync(
            RentedDomainsFiltersDto filters,
            Pagination<string> pagination,
            CancellationToken cancellationToken)
        {
            List<Expression<Func<RentedDomainModel, bool>>> filtersList = new List<Expression<Func<RentedDomainModel, bool>>>();

            var rented = await _domainsRepository.GetRentedAsync(
                filtersList,
                new Pagination<RentedDomainModel>(pagination.Page, pagination.Size), 
                cancellationToken
            );

            var domainTasks = rented.Collection.Select(async rd => await _domainsRepository.GetByIdAsync(rd.DomainId, cancellationToken));
            var domainsNames = (await Task.WhenAll(domainTasks)).Select(d => d.Name);

            var paginated = new PaginatedCollection<string>(
                    domainsNames,
                    rented.TotalCount,
                    rented.Page
                );

            return paginated;
        }

        public async Task<PaginatedCollection<GetDomainDto>> GetDomainsAsync(
            DomainFiltersDto domainFilters,
            Pagination<GetDomainDto> pagination, 
            CancellationToken cancellationToken)
        {
            List<Expression<Func<DomainModel, bool>>> filtersList = new();

            if (domainFilters.Name != null)
                filtersList.Add(
                    (d) => d.Name.StartsWith(domainFilters.Name)
                );

            var paginatedDomains = await _domainsRepository.GetAllAsync(
                filtersList, 
                new Pagination<DomainModel>(pagination.Page, pagination.Size),
                cancellationToken);

            return new PaginatedCollection<GetDomainDto>(paginatedDomains.Collection, paginatedDomains.TotalCount, paginatedDomains.Page);
        }

        public DomainsService(
            IDomainsRepository domainsRepository,
            IReceiptsRepository receiptsRepository,
            IValidator<RentDomainDto> rentDomainValidator)
        {
            _domainsRepository = domainsRepository;
            _receiptsRepository = receiptsRepository;

            _rentDomainValidator = rentDomainValidator;
        }
    }
}
