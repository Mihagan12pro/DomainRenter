namespace Contracts.Domains
{
    public record RentDomainDto(
            string DomainName,
            DateOnly EndRentDate
        );
}
