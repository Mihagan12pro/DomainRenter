using Contracts.Domains;
using CSharpFunctionalExtensions;
using Utils.Errors;
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
    }
}
