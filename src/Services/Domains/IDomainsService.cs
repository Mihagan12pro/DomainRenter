using Contracts.Domains;
using DomainModels.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Domains
{
    public interface IDomainsService
    {
        Task GetDomainsAsync(
            Expression<Func<bool, DomainModel>> filters,
            CancellationToken cancellationToken);

        Task<string> RentDomainAsync(
            RentDomainDto rentDomainDto,
            CancellationToken cancellationToken);
    }
}
