using CurrencyRateAdapter.Domain.Enums;

namespace CurrencyRateAdapter.Contracts.Providers.GetAllCurrencyRatesByProvider
{
    public record GetAllCurrencyRatesByProviderRequest(
        CurrencyProvider Provider,
        DateTime Date
    );
}
