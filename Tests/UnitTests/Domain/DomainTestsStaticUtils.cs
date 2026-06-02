using Contracts.Domains;
using Contracts.Users;
using Utils.Pagination;

namespace UnitTests.Domain
{
    public partial class DomainTests
    {
        public static IEnumerable<object[]> TestPaginationData()
        {
            return 
            [
                [
                    new Pagination<GetDomainDto>(1, 5),

                    5,//expected count on page

                    6//expected total count
                ],

                [
                    new Pagination<GetDomainDto>(2, 5),

                    1,//expected count on page

                    6//expected total count
                ],

                [
                    new Pagination<GetDomainDto>(1, 3),

                    3,//expected count on page

                    6//expected total count
                ],

                [
                    new Pagination<GetDomainDto>(1000, 5),

                    0,//expected count on page

                    6//expected total count
                ],
            ];
        }

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
