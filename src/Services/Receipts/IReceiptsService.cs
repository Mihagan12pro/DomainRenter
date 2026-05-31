using CSharpFunctionalExtensions;
using DomainModels.Receipts;
using Utils.Errors;
using Utils.Success;

namespace Services.Receipts
{
    public interface IReceiptsService
    {
        Task<Result<Success<ReceiptModel>, ErrorsCollection>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
