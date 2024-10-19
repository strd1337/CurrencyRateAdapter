using CurrencyRateAdapter.Application.Common.CQRS;
using CurrencyRateAdapter.Application.Common.Interfaces;
using CurrencyRateAdapter.Application.Providers.Common;
using ErrorOr;

namespace CurrencyRateAdapter.Application.Providers.Queries.GetAllAvailable
{
    public sealed class GetAllAvailableProvidersQueryHandler(
        ICurrencyProviderAdapter currencyProviderAdapter
    ) : IQueryHandler<GetAllAvailableProvidersQuery, GetAllAvailableProvidersResult>
    {
        public async Task<ErrorOr<GetAllAvailableProvidersResult>> Handle(
            GetAllAvailableProvidersQuery request,
            CancellationToken cancellationToken
        )
        {
            var result = await currencyProviderAdapter.GetAllAvailableProviderAsync();

            return result.IsError
                ? result.Errors
                : new GetAllAvailableProvidersResult(result.Value);
        }
    }
}
