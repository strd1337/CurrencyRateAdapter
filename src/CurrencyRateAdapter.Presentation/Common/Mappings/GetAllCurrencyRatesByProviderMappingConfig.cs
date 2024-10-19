using CurrencyRateAdapter.Application.Providers.Common;
using CurrencyRateAdapter.Application.Providers.Queries.GetAllCurrencyRatesByProvider;
using CurrencyRateAdapter.Contracts.Providers.GetAllCurrencyRatesByProvider;
using Mapster;

namespace CurrencyRateAdapter.Presentation.Common.Mappings
{
    public class GetAllCurrencyRatesByProviderMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetAllCurrencyRatesByProviderRequest, GetAllCurrencyRatesByProviderQuery>();
            config.NewConfig<GetAllCurrencyRatesByProviderResult, GetAllCurrencyRatesByProviderResponse>();
        }
    }
}
