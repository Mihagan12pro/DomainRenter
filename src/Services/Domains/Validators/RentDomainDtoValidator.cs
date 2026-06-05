using Contracts.Domains;
using FluentValidation;

namespace Services.Domains.Validators
{
    public class RentDomainDtoValidator : AbstractValidator<RentDomainDto>
    {
        public RentDomainDtoValidator()
        {
            DateTime now = DateTime.Now;

            RuleFor(dto => dto.DomainName.Contains('.'))
                .NotEqual(false)
                .WithMessage("Domain name can't be without any dots!");

            RuleFor(dto => Uri.CheckHostName(dto.DomainName))
                .Equal(UriHostNameType.Dns)
                .WithMessage("Invalid domain name!");

            RuleFor(dto => dto.EndRentDate.CompareTo(new DateOnly(now.Year, now.Month, now.Day)))
                .Equal(1)
                .WithMessage("Too late end rate!");
        }
    }
}
