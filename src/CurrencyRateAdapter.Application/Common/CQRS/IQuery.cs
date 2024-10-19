using ErrorOr;
using MediatR;

namespace CurrencyRateAdapter.Application.Common.CQRS
{
    public interface IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
    {
    }
}
