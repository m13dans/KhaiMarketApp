using Asp.Versioning;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace KhaiMarket.API.Controllers.V2
{
    [ApiVersion(2.0)]
    [ApiController]
    [Route("api/v{Version:ApiVersion}/[controller]")]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        protected IResult Problem(List<Error> errors)
        {
            var firstError = errors[0];

            var statusCode = firstError.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return Results.Problem(statusCode: statusCode,
                title: firstError.Description
            );
        }
    }
}