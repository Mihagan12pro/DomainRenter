using System.ComponentModel.DataAnnotations;

namespace Contracts.Users
{
    public record UserDto
    {
        [MinLength(3)]
        public string Name { get; init; }

        [MinLength(3)]
        public string Surname { get; init; }

        public string? Patronymic { get; init; }

        [EmailAddress]
        public string Email { get; init; }

        [Phone]
        public string Phone { get; init; }

        public UserDto(
            string name,
            string surname,
            string? patronymic,
            string email,
            string phone)
        {
            Name = name;

            Surname = surname;

            Patronymic = patronymic;

            Email = email;

            Phone = phone;
        }
    }
}