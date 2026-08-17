using FoodFlow.BuildingBlocks.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.BuildingBlocks.API.ApiResults;

internal static class ProblemDetailsFactory
{
    public static ProblemDetails ToProblemDetails(
        this Error error,
        HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        var timeProvider = httpContext.RequestServices.GetRequiredService<TimeProvider>();

        return new ProblemDetails
        {
            Status = error.Type.ToStatusCode(),
            Instance = httpContext.Request.Path,
            Extensions =
            {
                ["code"] = error.Code,
                ["errorMessages"] = error.Messages,
                ["applicationCode"] = error.ApplicationCode,
                ["errorType"] = error.Type.ToString(),
                ["timestamp"] = timeProvider.GetUtcNow().UtcDateTime,
                ["requestId"] = httpContext.TraceIdentifier
            }
        };
    }
}
