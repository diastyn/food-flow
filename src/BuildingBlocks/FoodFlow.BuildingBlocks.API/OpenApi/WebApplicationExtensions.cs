using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Scalar.AspNetCore;

namespace FoodFlow.BuildingBlocks.API.OpenApi;

public static class WebApplicationExtensions
{
    public static WebApplication UseScalarApiReference(this WebApplication app)
    {
        _ = app.MapScalarApiReference(options =>
        {
            _ = options
                .WithTitle("FoodFlow API")
                .AddPreferredSecuritySchemes(JwtBearerDefaults.AuthenticationScheme)
                .WithProxy(null!);
        });

        return app;
    }
}
