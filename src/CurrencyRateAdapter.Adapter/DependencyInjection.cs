using CurrencyRateAdapter.Adapter.Persistences;
using CurrencyRateAdapter.Adapter.Services;
using CurrencyRateAdapter.Application.Common.Interfaces;
using CurrencyRateAdapter.Application.Common.Services;
using CurrencyRateAdapter.Infrastructure.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyRateAdapter.Adapter
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAdapter(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services
                .AddSingleton<IDateTimeProvider, DateTimeProvider>()
                .AddScoped<ICurrencyProviderAdapter, CurrencyProviderAdapter>()
                .AddCurrencyApi(configuration)
                .AddHttpClient()
                ;

            return services;
        }

        public static IServiceCollection AddCurrencyApi(
           this IServiceCollection services,
           IConfiguration configuration
        )
        {
            CurrencyApiConfig currencyApiConfig = new();
            configuration.GetSection(CurrencyApiConfig.SectionName).Bind(currencyApiConfig);
            services.AddSingleton(currencyApiConfig);

            foreach (string provider in currencyApiConfig.CurrencyApiProviders)
            {
                if (currencyApiConfig.CurrencyApiSettings.TryGetValue(provider, out var apiSettings))
                {
                    services.AddHttpClient(provider, client =>
                    {
                        client.BaseAddress = new Uri(apiSettings.BaseUrl);
                        client.DefaultRequestHeaders.Add("Accept", "application/json");
                    });
                }
            }

            return services;
        }
    }
}
