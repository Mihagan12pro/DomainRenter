using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Domains
{
    public class RentedDomain
    {
        public Guid Id { get; }

        public required Guid DomainId { get; set; }

        [Column("from")]
        public required DateOnly StartOfRenting { get; set; }

        [Column("to")]
        public required DateOnly EndOfRenting { get; set; }
    }
}
