using CurrencyRateAdapter.Application.Common.CQRS;
using CurrencyRateAdapter.Application.Providers.Common;
using CurrencyRateAdapter.Domain.Enums;

namespace CurrencyRateAdapter.Application.Providers.Queries.GetAllCurrencyRatesByProvider
{
    public record GetAllCurrencyRatesByProviderQuery(
        CurrencyProvider Provider,
        DateTime Date
    ) : IQuery<GetAllCurrencyRatesByProviderResult>;
}
