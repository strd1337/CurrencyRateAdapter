using CurrencyRateAdapter.Domain.Dtos;
using ErrorOr;

namespace CurrencyRateAdapter.Application.Common.Interfaces
{
    public interface ICurrencyProviderAdapter
    {
        Task<ErrorOr<List<CurrencyProviderStatusDto>>> GetAllAvailableProvidersAsync(
            CancellationToken cancellationToken
        );

        Task<ErrorOr<List<CurrencyRateDto>>> GetAllNationalBankCurrencyRatesAsync(
            string provider,
            DateTime date,
            CancellationToken cancellationToken
        );
    }
}
