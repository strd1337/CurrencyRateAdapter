using CurrencyRateAdapter.Application.Common.CQRS;
using CurrencyRateAdapter.Application.Providers.Common;

namespace CurrencyRateAdapter.Application.Providers.Queries.GetAllAvailable
{
    public record GetAllAvailableProvidersQuery() : IQuery<GetAllAvailableProvidersResult>;
}
