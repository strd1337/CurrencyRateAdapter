using CurrencyRateAdapter.Domain.Dtos;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using CurrencyRateAdapter.Domain.Constants;
using CurrencyRateAdapter.Adapter.Persistences;
using ErrorOr;

namespace CurrencyRateAdapter.Adapter.Extensions
{
    public static class CurrencyRatesXmlExtensions
    {
        public static ErrorOr<List<CurrencyRateDto>> ParseXmlIntoCurrencyRates(
            this string xmlContent,
            ILogger<CurrencyProviderAdapter> logger
        )
        {
            List<CurrencyRateDto> rates = [];
            XDocument xmlDocument = XDocument.Parse(xmlContent);

            var currencyRates = xmlDocument.Descendants(Constants.CurrencyRateXmlElements.Valute);

            foreach (var rate in currencyRates)
            {
                try
                {
                    string? numCodeValue = rate.Element(Constants.CurrencyRateXmlElements.NumCode)?.Value;
                    string? nominalValue = rate.Element(Constants.CurrencyRateXmlElements.Nominal)?.Value;
                    string? valueValue = rate.Element(Constants.CurrencyRateXmlElements.Value)?.Value;
                    string? nameValue = rate.Element(Constants.CurrencyRateXmlElements.Name)?.Value ?? string.Empty;
                    string? charCodeValue = rate.Element(Constants.CurrencyRateXmlElements.CharCode)?.Value ?? string.Empty;

                    if (int.TryParse(numCodeValue, out int numCode) &&
                        int.TryParse(nominalValue, out int nominal) &&
                        decimal.TryParse(valueValue, out decimal value) &&
                        !string.IsNullOrEmpty(nameValue) &&
                        !string.IsNullOrEmpty(charCodeValue))
                    {
                        CurrencyRateDto currencyRate = new(
                            numCode,
                            charCodeValue,
                            nominal,
                            nameValue,
                            value
                        );
                        rates.Add(currencyRate);
                    }
                    else
                    {
                        logger.LogWarning(
                            @"Failed to parse currency rate for CharCode: {CharCode}.
                                NumCode: {NumCode}, Nominal: {Nominal}, Value: {Value}",
                            charCodeValue,
                            numCodeValue,
                            nominalValue,
                            valueValue
                        );
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError("Error parsing currency rate. {ErrorMessage}", ex.Message);
                    return Error.Unexpected("An error occurred while parsing currency rate.");
                }
            }

            return rates;
        }
    }
}
