using DataAccess.SQLite.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    internal class InMemeoryAppDbContext : AppDbContextBase
    {
        public InMemeoryAppDbContext(DbContextOptions<InMemeoryAppDbContext> options)
            : base(options)
        {
           
        }
    }
}
