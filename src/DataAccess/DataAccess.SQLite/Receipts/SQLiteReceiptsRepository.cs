using Contracts.Users;
using DataAccess.Abstractions.Receipts;
using DomainModels.Domains;
using DomainModels.Receipts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.SQLite.Receipts
{
    internal class SQLiteReceiptsRepository : IReceiptsRepository
    {
        private readonly AppDbContext _appDbContext;

        public async Task<Guid> AddAsync(
            Guid domainId,
            UserDto user,
            CancellationToken cancellationToken)
        {
            DomainModel domain = await _appDbContext.Domains.FirstOrDefaultAsync(d => d.Id == domainId);
            RentedDomainModel rentedDomain = await _appDbContext.RentedDomains.FirstOrDefaultAsync(rd => rd.DomainId == domainId);

            ReceiptModel receipt = new ReceiptModel()
            {
                DomainName = domain.Name,

                Email = user.Email,

                Phone = user.Phone,

                Name = user.Name,

                Surname = user.Surname,

                Patronymic = user.Patronymic,

                StartOfRenting = rentedDomain.StartOfRenting,

                EndOfRenting = rentedDomain.EndOfRenting,

                Price = (rentedDomain.EndOfRenting.DayNumber - rentedDomain.StartOfRenting.DayNumber) * 300
            };

            await _appDbContext.Receipts.AddAsync(receipt, cancellationToken);

            await _appDbContext.SaveChangesAsync();

            return receipt.Id;
        }

        public async Task<ReceiptModel> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            ReceiptModel receipt = await _appDbContext.Receipts.FirstOrDefaultAsync(r => r.Id == id);

            return receipt;
        }

        public SQLiteReceiptsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
