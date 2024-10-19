namespace CurrencyRateAdapter.Infrastructure.Configs
{
    public class CurrencyApiConfig
    {
        public const string SectionName = "CurrencyApiProviders";
        public List<string> CurrencyApiProviders { get; set; } = [];
        public Dictionary<string, CurrencyApiSettings> CurrencyApiSettings { get; set; } = [];
    }
}
