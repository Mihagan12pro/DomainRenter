using DataAccess.Abstractions.Domains;
using DomainModels.Domains;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.SQLite.Domains
{
    internal class SQLiteDomainsRepository : IDomainsRepository
    {
        private readonly AppDbContext _appContext;

        public SQLiteDomainsRepository(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Guid> AddAsync(string domainName, CancellationToken cancellationToken)
        {
            DomainModel domainModel = new DomainModel()
            {
                Name = domainName
            };

            await _appContext.AddAsync(domainModel, cancellationToken);
            await _appContext.SaveChangesAsync(cancellationToken);

            return domainModel.Id;
        }

        public async Task<DomainModel?> GetByNameAsync(
            string domainName,
            CancellationToken cancellationToken)
        {
            var domain = await _appContext.Domains.FirstOrDefaultAsync(d => d.Name == domainName);

            return domain;
        }

        public async Task<bool> IsRentedAsync(
            Guid id, 
            CancellationToken cancellationToken)
        {
            RentedDomainModel rentedDomain = await _appContext.RentedDomains.FirstOrDefaultAsync(rd => rd.DomainId == id);

            return rentedDomain != null;
        }

        public async Task<Guid> RentAsync(
            Guid id,
            DateOnly endRentDate,
            CancellationToken cancellationToken)
        {
            DateTime now = DateTime.UtcNow;

            RentedDomainModel rentedDomain = new RentedDomainModel()
            {
                StartOfRenting = new DateOnly(now.Year, now.Month, now.Day),

                EndOfRenting = endRentDate,

                DomainId = id
            };

            await _appContext.RentedDomains.AddAsync(rentedDomain, cancellationToken);

            await _appContext.SaveChangesAsync(cancellationToken);

            return rentedDomain.DomainId;
        }

        public async Task EndRentAsync(
            Guid id, 
            CancellationToken cancellationToken)
        {
            var model = await _appContext.RentedDomains.FirstOrDefaultAsync(rd => rd.Id == id, cancellationToken);

            _appContext.RentedDomains.Remove(model);

            await _appContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<DomainModel>> GetAllAsync(
            Expression<Func<DomainModel, bool>> filter, 
            CancellationToken cancellationToken)
        {
            var domains = _appContext.Domains.Where(filter);

            return domains;
        }

        public async Task<IEnumerable<RentedDomainModel>> GetRentedAsync(
            Expression<Func<RentedDomainModel, bool>> filter, 
            CancellationToken cancellationToken)
        {
            var rented = _appContext.RentedDomains.Where(filter);

            return rented;
        }
    }
}
