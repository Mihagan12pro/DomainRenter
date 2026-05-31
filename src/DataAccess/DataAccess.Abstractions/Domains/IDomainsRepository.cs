using Contracts.Domains;
using DomainModels.Domains;
using System.Linq.Expressions;
using Utils.Pagination;
using Utils.Pagination.Collections;
using Utils.Pagination.Collections.Types.Domains;

namespace DataAccess.Abstractions.Domains
{
    public interface IDomainsRepository
    {
        Task<Guid> AddAsync(
            string domainName, 
            CancellationToken cancellationToken);

        Task<DomainModel?> GetByNameAsync(
            string domainName,
            CancellationToken cancellationToken);

        Task<DomainModel> GetByIdAsync(
            Guid id, 
            CancellationToken cancellationToken);

        Task<bool> IsRentedAsync(
            Guid id,
            CancellationToken cancellationToken);

        Task<Guid> RentAsync(
            Guid id,
            DateOnly endRentDate,
            CancellationToken cancellationToken);

        Task EndRentAsync(
            Guid id,
            CancellationToken cancellationToken);

        public Task<PaginatedCollection<GetDomainDto>> GetAllAsync(
            IEnumerable<Expression<Func<DomainModel, bool>>> filters,
            Pagination<DomainModel> pagination,
            CancellationToken cancellationToken);

        public Task<PaginatedRentedDomains> GetRentedAsync(
            IEnumerable<Expression<Func<RentedDomainModel, bool>>> filters,
            Pagination<RentedDomainModel> pagination,
            CancellationToken cancellationToken);
    }
}
