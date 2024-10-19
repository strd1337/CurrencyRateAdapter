using CurrencyRateAdapter.Domain.Dtos;

namespace CurrencyRateAdapter.Contracts.Providers.GetAllCurrencyRatesByProvider
{
    public record GetAllCurrencyRatesByProviderResponse(
        List<CurrencyRateDto> CurrencyRates
    );
}
