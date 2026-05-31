using DataAccess.Abstractions.Receipts;
using Microsoft.AspNetCore.Mvc;
using Server.Extensions;
using Services.Receipts;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptsController : ControllerBase
    {
        private readonly IReceiptsService _receiptsService;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var result = await _receiptsService.GetByIdAsync(id, cancellationToken);

            if (result.IsFailure)
                return this.MapWithResult(result.Error);

            return this.MapWithResult(result.Value);
        }

        public ReceiptsController(IReceiptsService receiptsService)
        {
            _receiptsService = receiptsService;
        }
    }
}
