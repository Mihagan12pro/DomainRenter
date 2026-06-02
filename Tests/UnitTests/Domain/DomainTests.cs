using Contracts.Domains;
using Contracts.Users;
using DataAccess.SQLite.DbContexts;
using Microsoft.Extensions.DependencyInjection;
using Services.Domains;

namespace UnitTests.Domain
{
    public class DomainTests : UnitTests
    {
        private readonly DateOnly _now;

        [Theory]
        [MemberData(nameof(AddValidUsersDtos))]
        public async Task Test_AddSuccessAsync(UserDto user)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            RentDomainDto rentDomainDto = new RentDomainDto(
                "qwe.com",
                _now, 
                user
            );

            var provider = CreateProvider();

            var context = provider.GetRequiredService<AppDbContextBase>();

            IDomainsService domainsService = provider.GetRequiredService<IDomainsService>();

            var result = await domainsService.RentDomainAsync(rentDomainDto, cts.Token);

            Assert.Equal(true, result.IsSuccess);
        }

        public DomainTests()
        {
            DateTime dateTime = DateTime.Now.AddYears(1);

            _now = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
        }


        public static IEnumerable<object[]> AddValidUsersDtos()
        {
            return 
            [
                [
                     new UserDto(
                         "Иванов",
                         "Иван", 
                         "Иванович",
                         "email@test.ru",
                         "+7 900 900 90-90"
                     )
                ],
            ];
        }
    }
}
