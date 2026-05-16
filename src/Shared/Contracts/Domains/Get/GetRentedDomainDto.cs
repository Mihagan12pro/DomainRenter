namespace Contracts.Domains.Get
{
    public record GetRentedDomainDto : GetDomainDto
    {
        public DateOnly EndRentDate { get; init; }

        public GetRentedDomainDto(string Name, DateOnly EndRentDate) : base(Name)
        {
            this.EndRentDate = EndRentDate;
        }
    }
}
