using CurrencyRateAdapter.Domain.Providers;
using ErrorOr;

namespace CurrencyRateAdapter.Application.Common.Interfaces
{
    public interface ICurrencyProviderAdapter
    {
        Task<ErrorOr<List<CurrencyProviderStatusDto>>> GetAllAvailableProviderAsync();
    }
}
