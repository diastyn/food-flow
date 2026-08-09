using FoodFlow.BuildingBlocks.Results;
using Microsoft.AspNetCore.Mvc;

namespace FoodFlow.API;

internal static class ResultExtensions
{
    public static ActionResult ToActionResult<TValue>(
        this Result<TValue> result,
        HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);
        ArgumentNullException.ThrowIfNull(result);

        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Value);
        }

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Instance = httpContext.Request.Path,
            Extensions =
            {
                ["code"] = result.Error.Code,
                ["message"] = result.Error.Message,
                ["numericCode"] = result.Error.NumericCode,
                ["errorType"] = result.Error.Type.ToString(),
                ["timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                ["requestId"] = httpContext.TraceIdentifier
            }
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status,
        };
    }
}
