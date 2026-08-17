using FoodFlow.BuildingBlocks.API.ApiResults;
using FoodFlow.BuildingBlocks.Domain.Primitives;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ValidationException = FoodFlow.BuildingBlocks.Domain.Primitives.ValidationException;

namespace FoodFlow.BuildingBlocks.API.Middlewares;

public class ApplicationExceptionHandlingMiddleware(
    ILogger<ApplicationExceptionHandlingMiddleware> logger,
    RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (DomainException ex)
        {
            logger.LogError(ex, "Business exception occurred while executing request");

            await WriteProblemDetailsAsync(httpContext, ex.Error.ToProblemDetails(httpContext));
        }
        catch (ValidationException ex)
        {
            logger.LogError(ex, "Validation exception occurred while executing request");

            var problemDetails = ex.Error.ToProblemDetails(httpContext);
            problemDetails.Extensions["properties"] = ex.Properties;

            await WriteProblemDetailsAsync(httpContext, problemDetails);
        }
        catch (DbUpdateException ex)
        {
            // Например, гонка между параллельным check-then-insert и уникальным индексом в БД.
            logger.LogError(ex, "Database update conflict occurred while executing request");

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "The request could not be completed due to a conflicting change.",
                Instance = httpContext.Request.Path,
                Extensions =
                {
                    ["requestId"] = httpContext.TraceIdentifier
                }
            };

            await WriteProblemDetailsAsync(httpContext, problemDetails);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occurred while executing request");

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred.",
                Instance = httpContext.Request.Path,
                Extensions =
                {
                    ["requestId"] = httpContext.TraceIdentifier
                }
            };

            await WriteProblemDetailsAsync(httpContext, problemDetails);
        }
    }

    private static async Task WriteProblemDetailsAsync(
        HttpContext httpContext,
        ProblemDetails problemDetails)
    {
        if (httpContext.Response.HasStarted)
        {
            return;
        }

        httpContext.Response.StatusCode = problemDetails.Status!.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails);
    }
}
