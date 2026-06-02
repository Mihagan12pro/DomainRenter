using DataAccess.SQLite.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests
{
    public class TestDbContext : AppDbContextBase
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {

        }
    }
}
