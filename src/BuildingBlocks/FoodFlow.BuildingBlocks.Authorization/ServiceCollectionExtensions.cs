using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.BuildingBlocks.Authorization;

public static class ServiceCollectionExtensions
{
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
