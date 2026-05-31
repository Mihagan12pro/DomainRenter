using Contracts.Domains;
using CSharpFunctionalExtensions;
using System.Linq.Expressions;
using Utils.Errors;
using Utils.Pagination;
using Utils.Pagination.Collections;
using Utils.Success;

namespace Services.Domains
{
    public interface IDomainsService
    {
        Task<Result<Success, ErrorsCollection>> RentDomainAsync(
            RentDomainDto rentDomainDto,
            CancellationToken cancellationToken);

        Task<Result<Success<GetDomainDto>, ErrorsCollection>> GetByNameAsync(
            string name,
            CancellationToken cancellationToken);

        Task<Result<Success, ErrorsCollection>> EndRentAsync(
            string name,
            CancellationToken cancellationToken);

        Task<PaginatedCollection<string>> GetRentedDomainsAsync(
            RentedDomainsFiltersDto filters,
            Pagination<string> pagination,
            CancellationToken cancellationToken);

        Task<PaginatedCollection<GetDomainDto>> GetDomainsAsync(
            DomainFiltersDto? domainFilters,
            Pagination<GetDomainDto> pagination,
            CancellationToken cancellationToken);
    }
}
