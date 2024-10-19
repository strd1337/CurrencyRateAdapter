using CurrencyRateAdapter.Application.Common.CQRS;
using CurrencyRateAdapter.Application.Common.Interfaces;
using CurrencyRateAdapter.Application.Providers.Common;
using CurrencyRateAdapter.Domain.Enums;
using ErrorOr;

namespace CurrencyRateAdapter.Application.Providers.Queries.GetAllCurrencyRatesByProvider
{
    public sealed class GetAllCurrencyRatesByProviderQueryHandler(
        ICurrencyProviderAdapter adapter
    ) : IQueryHandler<GetAllCurrencyRatesByProviderQuery, GetAllCurrencyRatesByProviderResult>
    {
        public async Task<ErrorOr<GetAllCurrencyRatesByProviderResult>> Handle(
            GetAllCurrencyRatesByProviderQuery request,
            CancellationToken cancellationToken
        )
        {
            string provider = request.Provider.ToString();

            var result = request.Provider switch
            {
                CurrencyProvider.NationalBankOfMoldova => await adapter.GetAllNationalBankCurrencyRatesAsync(
                    provider,
                    request.Date,
                    cancellationToken
                ),
                _ => Error.NotFound($"The provider {request.Provider} is not supported.")
            };

            return result.IsError
                ? result.Errors
                : new GetAllCurrencyRatesByProviderResult(result.Value);
        }
    }
}
