using Contracts.Domains;
using Microsoft.AspNetCore.Mvc;
using Services.Domains;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DomainsController : ControllerBase
    {
        private readonly IDomainsService _domainsService;

        [HttpPost]
        public async Task<IActionResult> Rent(
            [FromBody] RentDomainDto request,
            CancellationToken cancellationToken)
        {
            var result = await _domainsService.RentDomainAsync(
                request,
                cancellationToken);

            if (result.IsSuccess)
                return Ok(result.Value);

            return Conflict(result.Fail);
        }

        public DomainsController(IDomainsService domainsService)
        {
            _domainsService = domainsService;
        }
    }
}
