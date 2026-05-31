using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Receipts
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
        public required string Phone { get; set; }

        [Column("from")]
        public required DateOnly StartOfRenting { get; set; }

        [Column("to")]
        public required DateOnly EndOfRenting { get; set; }

        public string CompanyName { get; private set; } = "ООО «Домены24»";

        public int INN { get; private set; } = 1234567890;

        public string CompanyAddress { get; private set; } = "г. N, ул. n, д. 1";

        public string CompanyEmail { get; private set; } = "renting@domains24.ru";

        public string CompanyPhone { get; private set; } = "+7 (xxx) xxx-xx-xx";

        public decimal Price { get; set; }
    }
}
