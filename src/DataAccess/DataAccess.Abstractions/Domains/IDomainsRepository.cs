using DomainModels.Domains;

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
    }
}
