using Contracts.Domains;
using Microsoft.AspNetCore.Mvc;
using Server.Extensions;
using Services.Domains;
using Utils.Pagination;

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

        [HttpDelete("{name}")]
        public async Task<IActionResult> EndRent(
            [FromRoute] string name,
            CancellationToken cancellationToken)
        {
            var result = await _domainsService.EndRentAsync(name, cancellationToken);

            if (result.IsFailure)
                return this.MapWithResult(result.Error);

            return this.MapWithResult(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetDomains(
            string? name,
            int page = 1,
            int size = 5,
            CancellationToken cancellationToken = default)
        {
            var result = await _domainsService.GetDomainsAsync(
                new DomainFiltersDto(name), 
                new Pagination<GetDomainDto>(page, size), 
                cancellationToken
            );

            return Ok(result);
        }

        [HttpGet("rented")]
        public async Task<IActionResult> GetRentedDomain(
            int page = 1,
            int size = 5,
            CancellationToken cancellationToken = default)
        {
            var result = await _domainsService.GetRentedDomainsAsync(
                new RentedDomainsFiltersDto(),
                new Pagination<string>(page, size),
                cancellationToken
            );

            return Ok(result);
        }

        public DomainsController(IDomainsService domainsService)
        {
            _domainsService = domainsService;
        }
    }
}
