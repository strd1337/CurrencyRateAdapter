using System.ComponentModel.DataAnnotations;
using CurrencyRateAdapter.Application.Providers.Queries.GetAllAvailable;
using CurrencyRateAdapter.Application.Providers.Queries.GetAllCurrencyRatesByProvider;
using CurrencyRateAdapter.Contracts.Providers.GetAllAvailable;
using CurrencyRateAdapter.Contracts.Providers.GetAllCurrencyRatesByProvider;
using CurrencyRateAdapter.Domain.Enums;
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

        [HttpGet("{provider}/currency-rates")]
        public async Task<IActionResult> GetAllCurrencyRatesByProvider(
            [FromRoute] CurrencyProvider provider,
            [Required][FromQuery] DateTime date,
            CancellationToken cancellationToken
        )
        {
            GetAllCurrencyRatesByProviderRequest request = new(provider, date);

            var query = mapper.Map<GetAllCurrencyRatesByProviderQuery>(request);

            var result = await mediator.Send(query, cancellationToken);

            return result.Match(
                result => Ok(mapper.Map<GetAllCurrencyRatesByProviderResponse>(result)),
                Problem
            );
        }
    }
}
