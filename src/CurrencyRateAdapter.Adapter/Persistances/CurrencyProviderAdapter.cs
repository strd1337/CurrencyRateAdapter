using CurrencyRateAdapter.Application.Common.Interfaces;
using CurrencyRateAdapter.Domain.Providers;
using CurrencyRateAdapter.Infrastructure.Configs;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace CurrencyRateAdapter.Adapter.Persistences
{
    public class CurrencyProviderAdapter(
        IHttpClientFactory httpClientFactory,
        CurrencyApiConfig currencyApiConfig,
        ILogger<CurrencyProviderAdapter> logger
    ) : ICurrencyProviderAdapter
    {
        public async Task<ErrorOr<List<CurrencyProviderStatusDto>>> GetAllAvailableProviderAsync()
        {
            var tasks = currencyApiConfig.CurrencyApiProviders.Select(async provider =>
            {
                bool isAvailable = false;

                try
                {
                    var client = httpClientFactory.CreateClient(provider);
                    var response = await client.GetAsync(string.Empty);

                    if (response.IsSuccessStatusCode)
                    {
                        isAvailable = true;
                    }
                    else
                    {
                        logger.LogWarning(
                            "Provider {Provider} responded with status code: {StatusCode}",
                            provider,
                            response.StatusCode
                        );
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(
                        "Error checking availability for provider {Provider}. {ErrorMessage}",
                        provider,
                        ex.Message
                    );
                }

                return new CurrencyProviderStatusDto(provider, isAvailable);
            });

            var availableProviders = await Task.WhenAll(tasks);

            return availableProviders.ToList();
        }
    }
}
