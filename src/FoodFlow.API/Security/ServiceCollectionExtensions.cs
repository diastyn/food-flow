using FoodFlow.BuildingBlocks.Authorization;
using FoodFlow.Modules.Identity.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FoodFlow.API.Security;

internal static class ServiceCollectionExtensions
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

    public static IServiceCollection AddPermissionAuthorization(
        this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        _ = services.AddAuthorization(config =>
        {
            foreach (var permission in AppPermissions.GetAll())
            {
                config.AddPolicy(permission, policy =>
                {
                    _ = policy.RequireClaim("permission", permission);
                });
            }
        });

        return services;
    }
}
