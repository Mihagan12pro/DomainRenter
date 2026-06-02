using Contracts.Domains;
using Contracts.Users;
using DataAccess.SQLite.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using Services.Domains;

namespace UnitTests.Domain
{
    public partial class DomainTests : UnitTests
    {
        private readonly DateOnly _now;

        [Fact]
        public async Task Test_GetByNameAsync()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            UserDto user = new UserDto(
                         "Иванов",
                         "Иван",
                         "Иванович",
                         "email@test.ru",
                         "+7 900 900 90-90"
            );

            RentDomainDto rentDomainDto = new RentDomainDto(
                "qwe.com",
                _now,
                user
            );

            var provider = CreateProvider();

            IDomainsService domainsService = provider.GetRequiredService<IDomainsService>();

            var result1 = await domainsService.RentDomainAsync(rentDomainDto, cts.Token);

            var result2 = await domainsService.GetByNameAsync(rentDomainDto.DomainName, cts.Token);

            Assert.True(result2.IsSuccess);
        }

        [Theory]
        [MemberData(nameof(AddValidUsersDtos))]
        public async Task Test_AddSuccessAsync(string domainName, UserDto user)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            RentDomainDto rentDomainDto = new RentDomainDto(
                domainName,
                _now, 
                user
            );

            var provider = CreateProvider();

            IDomainsService domainsService = provider.GetRequiredService<IDomainsService>();

            var result = await domainsService.RentDomainAsync(rentDomainDto, cts.Token);

            Assert.True(result.IsSuccess);
        }

        public DomainTests()
        {
            DateTime dateTime = DateTime.Now.AddYears(1);

            _now = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
        }
    }
}
