using Contracts.Users;
using DomainModels.Receipts;

namespace DataAccess.Abstractions.Receipts
{
    public interface IReceiptsRepository
    {
        /// <summary>
        /// Add new receipt to db
        /// </summary>
        /// <param name="domainId"></param>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Receipt id</returns>
        Task<Guid> AddAsync(
            Guid domainId,
            UserDto user, 
            CancellationToken cancellationToken);

        Task<ReceiptModel> GetByIdAsync(
            Guid id, 
            CancellationToken cancellationToken);
    }
}
