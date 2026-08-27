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
}
