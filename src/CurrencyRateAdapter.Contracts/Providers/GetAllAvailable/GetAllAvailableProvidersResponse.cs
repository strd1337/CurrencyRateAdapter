using CurrencyRateAdapter.Domain.Dtos;

namespace CurrencyRateAdapter.Contracts.Providers.GetAllAvailable
{
    public record GetAllAvailableProvidersResponse(
        List<CurrencyProviderStatusDto> Providers
    );
}
