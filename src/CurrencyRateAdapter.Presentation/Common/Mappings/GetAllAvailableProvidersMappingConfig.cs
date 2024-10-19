using CurrencyRateAdapter.Application.Providers.Common;
using CurrencyRateAdapter.Application.Providers.Queries.GetAllAvailable;
using CurrencyRateAdapter.Contracts.Providers.GetAllAvailable;
using Mapster;

namespace CurrencyRateAdapter.Presentation.Common.Mappings
{
    public sealed class GetAllAvailableProvidersMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetAllAvailableProvidersRequest, GetAllAvailableProvidersQuery>();
            config.NewConfig<GetAllAvailableProvidersResult, GetAllAvailableProvidersResponse>();
        }
    }
}
