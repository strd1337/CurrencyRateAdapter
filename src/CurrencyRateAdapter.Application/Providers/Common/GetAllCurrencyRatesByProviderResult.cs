using CurrencyRateAdapter.Domain.Dtos;

namespace CurrencyRateAdapter.Application.Providers.Common
{
    public record GetAllCurrencyRatesByProviderResult(
        List<CurrencyRateDto> CurrencyRates
    );
}
