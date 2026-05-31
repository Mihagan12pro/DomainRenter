using DataAccess.Abstractions.Domains;
using DomainModels.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using Utils.Pagination;
using Utils.Pagination.Collections.Types.Domains;

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

            RentedDomainModel rentedDomain = await _appContext.RentedDomains.FirstOrDefaultAsync(rd => rd.DomainId == id);

            if (rentedDomain != null)
            {
                _appContext.RentedDomains.Remove(rentedDomain);

                await _appContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<PaginatedDomains> GetAllAsync(
            IEnumerable<Expression<Func<DomainModel, bool>>> filters,
            Pagination<DomainModel> pagination, CancellationToken cancellationToken)
        {
            IQueryable<DomainModel> domains = _appContext.Domains;

            foreach (var filter in filters)
                domains = domains.Where(filter);

            IEnumerable<DomainModel> list = domains.ToList();
            int totalCount = list.Count();

            list = pagination.Apply(list);

            PaginatedDomains paginatedDomains = new PaginatedDomains(
                list,
                totalCount,
                pagination.Page);
            
            return paginatedDomains;
        }

        public async Task<PaginatedRentedDomains> GetRentedAsync(
            IEnumerable<Expression<Func<RentedDomainModel, bool>>> filters, 
            Pagination<RentedDomainModel> pagination, CancellationToken cancellationToken)
        {
            IQueryable<RentedDomainModel> rentedDomains = _appContext.RentedDomains;

            foreach(var filter in filters)
                rentedDomains = rentedDomains.Where(filter);

            IEnumerable<RentedDomainModel> list = rentedDomains.ToList();
            int totalCount = list.Count();

            list = pagination.Apply(list);

            PaginatedRentedDomains paginatedRentedDomains = new PaginatedRentedDomains(
                list,
                totalCount,
                pagination.Page);

            return paginatedRentedDomains;
        }

        public async Task<DomainModel> GetByIdAsync(
            Guid id, 
            CancellationToken cancellationToken)
        {
            DomainModel model = await _appContext.Domains.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

            return model;
        }
    }
}
