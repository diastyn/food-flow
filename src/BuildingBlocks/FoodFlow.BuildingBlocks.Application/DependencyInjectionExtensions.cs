using System.Reflection;
using FluentValidation;
using FoodFlow.BuildingBlocks.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.BuildingBlocks.Application;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection RegisterApplicationLayerDefaults(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(assemblies);

        _ = services.AddMediatR(cfg =>
        {
            _ = cfg.RegisterServicesFromAssemblies(assemblies);
            _ = cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        _ = services.AddValidatorsFromAssemblies(assemblies);

        return services;
    }
}
