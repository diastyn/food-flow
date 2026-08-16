using FoodFlow.BuildingBlocks.Results;
using Microsoft.AspNetCore.Mvc;

namespace FoodFlow.API.ApiResults;

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

        var problemDetails = result.Error.ToProblemDetails(httpContext);

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status,
        };
    }

    public static ActionResult ToActionResult(
        this Result result,
        HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);
        ArgumentNullException.ThrowIfNull(result);

        if (result.IsSuccess)
        {
            return new OkResult();
        }

        var problemDetails = result.Error.ToProblemDetails(httpContext);

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status,
        };
    }
}
