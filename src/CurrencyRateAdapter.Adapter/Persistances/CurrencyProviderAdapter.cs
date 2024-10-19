using System.Web;
using System.Xml.Linq;
using CurrencyRateAdapter.Adapter.Extensions;
using CurrencyRateAdapter.Application.Common.Interfaces;
using CurrencyRateAdapter.Domain.Constants;
using CurrencyRateAdapter.Domain.Dtos;
using CurrencyRateAdapter.Domain.Enums;
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
        public async Task<ErrorOr<List<CurrencyProviderStatusDto>>> GetAllAvailableProvidersAsync(
            CancellationToken cancellationToken
        )
        {
            var tasks = currencyApiConfig.CurrencyApiProviders.Select(async provider =>
            {
                bool isAvailable = false;

                try
                {
                    var client = httpClientFactory.CreateClient(provider);
                    var response = await client.GetAsync(string.Empty, cancellationToken);

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

                    string description = currencyApiConfig.CurrencyApiSettings[provider].Description;

                    return new CurrencyProviderStatusDto(provider, description, isAvailable);
                }
                catch (Exception ex)
                {
                    logger.LogError(
                        "Error checking availability for provider {Provider}. {ErrorMessage}",
                        provider,
                        ex.Message
                    );
                    throw;
                }
            });

            var availableProviders = await Task.WhenAll(tasks);

            return availableProviders.ToList();
        }

        public async Task<ErrorOr<List<CurrencyRateDto>>> GetAllNationalBankCurrencyRatesAsync(
            string provider,
            DateTime date,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var queryString = HttpUtility.ParseQueryString(string.Empty);

                queryString.Add(nameof(date), $"{date:dd.MM.yyyy}");

                string url = $"{Constants.Url.Get(
                    CurrencyProvider.NationalBankOfMoldova,
                    currencyApiConfig.CurrencyApiSettings[provider].BaseUrl)}{queryString}";

                var client = httpClientFactory.CreateClient(provider);

                var response = await client.GetAsync(url, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return response.StatusCode.GetResponse();
                }

                string content = await response.Content.ReadAsStringAsync(cancellationToken);

                var currencyRates = content.ParseXmlIntoCurrencyRates(logger);

                return currencyRates;
            }
            catch (Exception ex)
            {
                logger.LogError("Error fetching National Bank currency rates: {ErrorMessage}", ex.Message);
                return Error.Unexpected("An error occurred while fetching currency rates.");
            }
        }
    }
}
