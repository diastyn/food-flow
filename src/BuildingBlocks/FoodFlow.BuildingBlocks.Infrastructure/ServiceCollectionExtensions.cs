using FoodFlow.BuildingBlocks.Domain.Security;
using FoodFlow.BuildingBlocks.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.BuildingBlocks.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterTimeProvider(
        this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        _ = services.AddSingleton(TimeProvider.System);

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
}
