namespace CurrencyRateAdapter.Domain.Dtos
{
    public record CurrencyProviderStatusDto(
        string Name,
        string Description,
        bool IsAvailable
    );
}
