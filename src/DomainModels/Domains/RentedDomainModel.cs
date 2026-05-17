using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DomainModels.Domains
{

    public class RentedDomainModel
    {
        public Guid Id { get; private set; }

        public required Guid DomainId { get; set; }

        [ForeignKey(nameof(DomainId)), JsonIgnore]
        public DomainModel Domain { get; set; }

        [Column("from")]
        public required DateOnly StartOfRenting { get; set; }

        [Column("to")]
        public required DateOnly EndOfRenting { get; set; }
    }
}
