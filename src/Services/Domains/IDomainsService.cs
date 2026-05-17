using Contracts.Domains;
using DomainModels.Domains;
using HelpEntities;
using System.Linq.Expressions;

namespace Services.Domains
{
    public interface IDomainsService
    {
        Task GetDomainsAsync(
            Expression<Func<bool, DomainModel>> filters,
            CancellationToken cancellationToken);

        Task<Result<string, string>> RentDomainAsync(
            RentDomainDto rentDomainDto,
            CancellationToken cancellationToken);
    }
}
