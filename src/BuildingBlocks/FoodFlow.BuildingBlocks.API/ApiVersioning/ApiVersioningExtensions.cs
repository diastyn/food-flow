using Asp.Versioning;
using FoodFlow.BuildingBlocks.API.OpenApi;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.BuildingBlocks.API.ApiVersioning;

public static class ApiVersioningExtensions
{
    public static IServiceCollection AddRestApiVersioning(
        this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        _ = services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = false;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })
            .AddOpenApi(options =>
            {
                _ = options.Document.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
                _ = options.Document.AddOperationTransformer<BearerSecurityRequirementTransformer>();
            });

        return services;
    }
}
