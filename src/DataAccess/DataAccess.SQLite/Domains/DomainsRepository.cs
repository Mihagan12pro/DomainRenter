using DataAccess.Abstractions.Domains;

namespace DataAccess.SQLite.Domains
{
    internal class DomainsRepository : IDomainsRepository
    {
        private readonly AppDbContext _appContext;

        public DomainsRepository(AppDbContext appContext)
        {
            _appContext = appContext;
        }
    }
}
