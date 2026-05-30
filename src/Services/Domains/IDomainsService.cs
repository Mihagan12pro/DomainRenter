using Contracts.Domains;
using CSharpFunctionalExtensions;
using DomainModels.Domains;
using System.Linq.Expressions;
using Utils.Errors;
using Utils.Success;

namespace Services.Domains
{
    public interface IDomainsService
    {
        Task GetDomainsAsync(
            Expression<Func<bool, DomainModel>> filters,
            CancellationToken cancellationToken);

        Task<Result<Success<string>, ErrorsCollection>> RentDomainAsync(
            RentDomainDto rentDomainDto,
            CancellationToken cancellationToken);
    }
}
