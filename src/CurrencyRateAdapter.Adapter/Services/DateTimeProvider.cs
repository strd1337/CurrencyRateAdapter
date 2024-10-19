using CurrencyRateAdapter.Application.Common.Services;

namespace CurrencyRateAdapter.Adapter.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
