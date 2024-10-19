using System.Net;
using ErrorOr;

namespace CurrencyRateAdapter.Adapter.Extensions
{
    public static class HttpStatusCodeExtensions
    {
        public static Error GetResponse(this HttpStatusCode httpStatusCode)
            => httpStatusCode switch
            {
                HttpStatusCode.BadRequest => Error.Validation("The request was invalid."),
                HttpStatusCode.Forbidden => Error.Forbidden("Access to the resource is denied."),
                HttpStatusCode.NotFound => Error.NotFound("The requested resource could not be found."),
                HttpStatusCode.InternalServerError => Error.Unexpected("An error occurred on the server."),
                _ => Error.Failure($"Failed to fetch data. Status code: {httpStatusCode}")
            };
    }
}
