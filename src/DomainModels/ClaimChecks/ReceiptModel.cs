using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.ClaimChecks
{
    public class ReceiptModel
    {
        public Guid Id { get; private set; }

        public required string DomainName { get; set; }

        public required string Name { get; set; }

        public required string Surname { get; set; }

        public string? Patronymic { get; set; }

        [EmailAddress()]
        public required string Email { get; set; }

        [Phone()]
        public required string PhoneNumber { get; set; }

        [Column("from")]
        public required DateOnly StartOfRenting { get; set; }

        [Column("to")]
        public required DateOnly EndOfRenting { get; set; }

        public string CompanyName { get; } = "ООО «Домены24»";

        public int INN { get; } = 1234567890;

        public string CompanyAddress = "г. N, ул. n, д. 1";

        public string CompanyEmail { get; } = "renting@domains24.ru";

        public string CompanyPhone { get; } = "+7 (xxx) xxx-xx-xx";
    }
}
