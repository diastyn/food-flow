using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.BuildingBlocks.Domain.Mapper;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection RegisterMapper(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(services);

        _ = services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(assemblies);
        }, assemblies);

        return services;
    }
}
