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

        public async Task<IActionResult> Rent(
            string domainName, 
            DateOnly endRent,
            CancellationToken cancellationToken)
        {
            await _domainsService.RentDomainAsync(
                new RentDomainDto(domainName, endRent),
                cancellationToken);

            return Ok();
        }

        public DomainsController(IDomainsService domainsService)
        {
            _domainsService = domainsService;
        }
    }
}
