using Contracts.Users;

namespace Contracts.Domains
{
    public record RentDomainDto(
            string DomainName,
            DateOnly EndRentDate,
            UserDto UserDto
        );
}
