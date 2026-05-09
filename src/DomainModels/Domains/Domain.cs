using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Domains
{
    public class Domain
    {
        public Guid Id { get; }

        public required string Name { get; set; }
    }
}
