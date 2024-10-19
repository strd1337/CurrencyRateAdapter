namespace CurrencyRateAdapter.Domain.Providers
{
    public record CurrencyProviderStatusDto(
        string Name,
        bool IsAvailable
    );
}
