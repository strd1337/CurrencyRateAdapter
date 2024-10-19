using CurrencyRateAdapter.Domain.Enums;

namespace CurrencyRateAdapter.Domain.Constants
{
    public static partial class Constants
    {
        public static partial class Url
        {
            public static string Get(CurrencyProvider provider, string baseUrl) => provider switch
            {
                CurrencyProvider.NationalBankOfMoldova => $"{baseUrl}/official_exchange_rates?get_xml=1&",
                _ => string.Empty
            };
        }
    }
}
