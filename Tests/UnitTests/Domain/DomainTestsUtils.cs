using Contracts.Domains;
using Contracts.Users;

namespace UnitTests.Domain
{
    public partial class DomainTests
    {
        public IEnumerable<RentDomainDto> AddRentedDomains()
        {
            List<RentDomainDto> rentDomainDtos = new List<RentDomainDto>()
            {
                new RentDomainDto(
                    "qwe.com", 
                    _now,
                    new UserDto(
                        "Иванов", 
                        "Иван", 
                        null, 
                        "example@email.com", 
                        "+7 900 900 90-89"
                        )
                    ),

                new RentDomainDto(
                    "qwe.ru",
                    _now,
                    new UserDto(
                        "Иванов",
                        "Иван",
                        null,
                        "example@email.com",
                        "+7 900 900 90-89"
                        )
                    ),

                new RentDomainDto(
                    "qwe.su",
                    _now,
                    new UserDto(
                        "Иванов",
                        "Иван",
                        null,
                        "example@email.com",
                        "+7 900 900 90-89"
                        )
                    ),

                new RentDomainDto(
                    "qwe.gov",
                    _now,
                    new UserDto(
                        "Иванов",
                        "Иван",
                        null,
                        "example@email.com",
                        "+7 900 900 90-89"
                        )
                    ),

                new RentDomainDto(
                    "qwe.public.com",
                    _now,
                    new UserDto(
                        "Иванов",
                        "Иван",
                        null,
                        "example@email.com",
                        "+7 900 900 90-89"
                        )
                    ),

                new RentDomainDto(
                    "qwe.public.su",
                    _now,
                    new UserDto(
                        "Иванов",
                        "Иван",
                        null,
                        "example@email.com",
                        "+7 900 900 90-89"
                        )
                    ),
            };

            return rentDomainDtos;
        }
    }
}
