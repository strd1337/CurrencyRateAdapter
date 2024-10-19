using ErrorOr;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using CurrencyRateAdapter.Presentation.Common.Http;
using Microsoft.AspNetCore.RateLimiting;
using CurrencyRateAdapter.Domain.Constants;

namespace CurrencyRateAdapter.Presentation.Controllers.Common
{
    [ApiController]
    [EnableRateLimiting(Constants.TokenBucketLimiter.PolicyName.GlobalLimit)]
    public class ApiController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {
            if (errors.Count is 0)
            {
                return Problem();
            }

            if (errors.All(error => error.Type == ErrorType.Validation))
            {
                return ValidationProblem(errors);
            }

            HttpContext.Items[HttpContextItemKeys.Errors] = errors;

            return Problem(errors[0]);
        }

        private ActionResult ValidationProblem(List<Error> errors)
        {
            ModelStateDictionary modelStateDictionary = new();

            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(
                    error.Code,
                    error.Description
                );
            }

            return ValidationProblem(modelStateDictionary);
        }

        private ObjectResult Problem(Error error)
        {
            int statusCode = error.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Failure => StatusCodes.Status500InternalServerError,
                ErrorType.Unexpected => StatusCodes.Status500InternalServerError,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError,
            };

            return Problem(statusCode: statusCode, title: error.Description);
        }
    }
}
