using Contracts.Domains;
using Contracts.Users;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests.Domains
{
    public partial class DomainsIntegrationTests : IntegrationTests
    {
        [Fact]
        public async Task Test_RentRentedDomain()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            DateTime dateTime = DateTime.Now.AddDays(1);
            DateOnly now = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);

            RentDomainDto rentDomainDto = new RentDomainDto(
                
                "qwe.com",

                now,

                new UserDto(
                    "Иван", 
                    "Иванов",
                    "Иванович",
                    "sample@mail.ru", 
                    "+7 800 800 90-10"
                )
            );

            await httpClient.PostAsJsonAsync<RentDomainDto>("api/domains", rentDomainDto, cts.Token);

            var result = await httpClient.PostAsJsonAsync<RentDomainDto>("api/domains", rentDomainDto, cts.Token);

            Assert.Equal(HttpStatusCode.Conflict, result.StatusCode);
        }

        [Fact]
        public async Task Test_Deleting()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            DateTime dateTime = DateTime.Now.AddDays(1);
            DateOnly now = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);

            RentDomainDto rentDomainDto = new RentDomainDto(

                "qwe.com",

                now,

                new UserDto(
                    "Иван",
                    "Иванов",
                    "Иванович",
                    "sample@mail.ru",
                    "+7 800 800 90-10"
                )
            );

            await httpClient.PostAsJsonAsync<RentDomainDto>("api/domains", rentDomainDto, cts.Token);

            await httpClient.DeleteAsync($"api/domains/{rentDomainDto.DomainName}", cts.Token);

            var result = await httpClient.PostAsJsonAsync<RentDomainDto>("api/domains", rentDomainDto, cts.Token); ;

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Theory]
        [MemberData(nameof(TestRentBadRequestData))]
        public async Task Test_RentWithBadRequest(RentDomainDto rentDomainDto)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            var result = await httpClient.PostAsJsonAsync<RentDomainDto>("api/domains", rentDomainDto, cts.Token);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        public DomainsIntegrationTests(DomainRenterAppFactory<Program> factory) : base(factory)
        {
        }
    }
}
