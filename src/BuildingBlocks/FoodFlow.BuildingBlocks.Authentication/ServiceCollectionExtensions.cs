using FoodFlow.BuildingBlocks.Domain.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FoodFlow.BuildingBlocks.Authentication;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityAuthentication(
        this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        _ = services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        _ = services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
            .Configure<RsaSigningKeyProvider, IOptions<JwtOptions>>((bearer, provider, options) =>
            {
                var jwtOptions = options.Value;

                bearer.MapInboundClaims = false;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = provider.SecurityKey,
                    RoleClaimType = "role",
                    NameClaimType = "preferred_username"
                };
            });

        return services;
    }

    public static IServiceCollection RegisterHttpRequestContext(
        this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        _ = services.AddHttpContextAccessor();
        _ = services.AddScoped<IRequestContext, HttpRequestContext>();

        return services;
    }

    public static IServiceCollection RegisterJwtAuthenticationDefaults(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        _ = services
            .AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        _ = services.AddSingleton<RsaSigningKeyProvider>();

        return services;
    }
}
