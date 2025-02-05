using ErrorOr;
using MediatR;

namespace CurrencyRateAdapter.Application.Common.CQRS
{
    public interface IQueryHandler<TQuery, TResponse>
         : IRequestHandler<TQuery, ErrorOr<TResponse>>
             where TQuery : IQuery<TResponse>
    {
    }
}
