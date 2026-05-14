using DataAccess.Abstractions.Domains;

namespace DataAccess.SQLite.Domains
{
    internal class RentedDomainsRepository : IRentedDomainsRepository
    {
        private readonly AppDbContext _appContext;

        public RentedDomainsRepository(AppDbContext appContext)
        {
            _appContext = appContext;
        }
    }
}
