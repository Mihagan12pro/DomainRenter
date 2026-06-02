using Contracts.Users;

namespace UnitTests.Domain
{
    public partial class DomainTests
    {
        public static IEnumerable<object[]> AddValidUsersDtos()
        {
            return
            [
                [
                    "qwe.com",

                     new UserDto(
                         "Иванов",
                         "Иван",
                         "Иванович",
                         "email@test.ru",
                         "+7 900 900 90-90"
                     )
                ],

                [
                    "qwe.com",

                     new UserDto(
                         "Иванов",
                         "Иван",
                         null,
                         "email@test.ru",
                         "+7 900 900 90-90"
                     )
                ],

                [
                    "qwe.com",

                     new UserDto(
                         "иванов",
                         "иван",
                         null,
                         "email@test.ru",
                         "+7 900 900 90-90"
                     )
                ],

                [
                    "qwe.рф",

                     new UserDto(
                         "Иванов",
                         "Иван",
                         null,
                         "email@test.ru",
                         "+7 900 900 90-90"
                     )
                ],

                [
                    "qwe.com",

                     new UserDto(
                         "John",
                         "Smith",
                         null,
                         "email@test.ru",
                         "+7 900 900 90-90"
                     )
                ],
            ];
        }
    }
}
