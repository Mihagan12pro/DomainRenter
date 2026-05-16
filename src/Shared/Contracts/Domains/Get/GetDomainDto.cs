namespace Contracts.Domains.Get
{
    public record GetDomainDto
    {
        public string Name { get; init; }


        public GetDomainDto(string Name)
        {
            this.Name = Name;
        }
    }
}
