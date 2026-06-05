using Contracts.Domains;
using Contracts.Users;

namespace IntegrationTests.Domains
{
    public partial class DomainsIntegrationTests
    {
        public static IEnumerable<object[]> TestRentBadRequestData()
        {
            DateTime dateTime = DateTime.Now.AddDays(1);
            DateOnly now = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);

            return
            [
                [
                    new RentDomainDto(

                        "qwecom",

                        now,

                        new UserDto(
                            "Иван",
                            "Иванов",
                            "Иванович",
                            "sample@mail.ru",
                            "+7 800 800 90-10"
                        )
                    )
                ],

                [
                    new RentDomainDto(

                        "111.111",

                        now,

                        new UserDto(
                            "Иван",
                            "Иванов",
                            "Иванович",
                            "sample@mail.ru",
                            "+7 800 800 90-10"
                        )
                    )
                ],

                [
                    new RentDomainDto(

                        "qwe.com",

                        now,

                        new UserDto(
                            string.Empty,
                            "Иванов",
                            "Иванович",
                            "sample@mail.ru",
                            "+7 800 800 90-10"
                        )
                    )
                ],

                [
                    new RentDomainDto(

                        "qwe.com",

                        now,

                        new UserDto(
                            "Иван",
                            string.Empty,
                            "Иванович",
                            "sample@mail.ru",
                            "+7 800 800 90-10"
                        )
                    )
                ],

                [
                    new RentDomainDto(

                        "qwe.com",

                        now,

                        new UserDto(
                            "Иван",
                            "Иванов",
                            "Иванович",
                            "samplemailru",
                            "+7 800 800 90-10"
                        )
                    )
                ],

                [
                    new RentDomainDto(

                        "qwe.com",

                        now,

                        new UserDto(
                            "Иван",
                            "Иванов",
                            "Иванович",
                            string.Empty,
                            "+7 800 800 90-10"
                        )
                    )
                ],

                [
                    new RentDomainDto(

                        "qwe.com",

                        new DateOnly(2000, 10, 10),

                        new UserDto(
                            "Иван",
                            "Иванов",
                            "Иванович",
                            "sample@mail.ru",
                            "+7 800 800 90-10"
                        )
                    )
                ],
            ];
        }
    }
}
