using Contracts.Domains;
using Microsoft.AspNetCore.Mvc;
using Server.Extensions;
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

            if (result.IsFailure)
                return this.MapWithResult(result.Error);

            return this.MapWithResult(result.Value);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(
            [FromRoute] string name, 
            CancellationToken cancellationToken)
        {
            var result = await _domainsService.GetByNameAsync(name, cancellationToken);

            if (result.IsFailure)
                return this.MapWithResult(result.Error);

            return this.MapWithResult(result.Value);
        }

        public DomainsController(IDomainsService domainsService)
        {
            _domainsService = domainsService;
        }
    }
}
