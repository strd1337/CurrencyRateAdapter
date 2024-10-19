using CurrencyRateAdapter.Domain.Providers;

namespace CurrencyRateAdapter.Application.Providers.Common
{
    public record GetAllAvailableProvidersResult(
        List<CurrencyProviderStatusDto> Providers
    );
}
