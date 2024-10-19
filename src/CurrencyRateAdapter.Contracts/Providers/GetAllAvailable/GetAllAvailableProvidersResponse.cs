using CurrencyRateAdapter.Domain.Providers;

namespace CurrencyRateAdapter.Contracts.Providers.GetAllAvailable
{
    public record GetAllAvailableProvidersResponse(
        List<CurrencyProviderStatusDto> Providers
    );
}
