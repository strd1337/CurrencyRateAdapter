namespace CurrencyRateAdapter.Domain.Dtos
{
    public record CurrencyRateDto(
        int NumCode,
        string CharCode,
        int Nominal,
        string Name,
        decimal Value
    );
}
