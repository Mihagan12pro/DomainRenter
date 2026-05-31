using CSharpFunctionalExtensions;
using DataAccess.Abstractions.Receipts;
using DomainModels.Receipts;
using Utils.Errors;
using Utils.Success;

namespace Services.Receipts
{
    internal class ReceiptsService : IReceiptsService
    {
        private readonly IReceiptsRepository _receiptsRepository;

        public async Task<Result<Success<ReceiptModel>, ErrorsCollection>> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            ReceiptModel receipt = await _receiptsRepository.GetByIdAsync(id, cancellationToken);

            if (receipt == null)
                return new ErrorsCollection(404, "This receipt does not exists!");

            return new Success<ReceiptModel>(receipt);
        }

        public ReceiptsService(IReceiptsRepository receiptsRepository)
        {
            _receiptsRepository = receiptsRepository;
        }
    }
}
