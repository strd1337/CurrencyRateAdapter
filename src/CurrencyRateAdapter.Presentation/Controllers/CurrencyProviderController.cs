using CurrencyRateAdapter.Application.Providers.Queries.GetAllAvailable;
using CurrencyRateAdapter.Contracts.Providers.GetAllAvailable;
using CurrencyRateAdapter.Presentation.Controllers.Common;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyRateAdapter.Presentation.Controllers
{
    [Route("api/providers")]
    [ApiController]
    public class CurrencyProviderController(
        IMediator mediator,
        IMapper mapper
    ) : ApiController
    {
        [HttpGet("status")]
        public async Task<IActionResult> GetAllAvailableProviders(
            CancellationToken cancellationToken
        )
        {
            GetAllAvailableProvidersRequest request = new();

            var query = mapper.Map<GetAllAvailableProvidersQuery>(request);

            var result = await mediator.Send(query, cancellationToken);

            return result.Match(
                result => Ok(mapper.Map<GetAllAvailableProvidersResponse>(result)),
                Problem
            );
        }
    }
}
