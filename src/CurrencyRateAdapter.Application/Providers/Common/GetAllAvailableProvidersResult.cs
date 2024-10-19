using CurrencyRateAdapter.Domain.Dtos;

namespace CurrencyRateAdapter.Application.Providers.Common
{
    public record GetAllAvailableProvidersResult(
        List<CurrencyProviderStatusDto> Providers
    );
}
