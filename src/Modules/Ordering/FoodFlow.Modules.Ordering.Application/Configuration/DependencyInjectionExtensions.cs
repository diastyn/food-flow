using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Modules.Ordering.Application.Configuration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection RegisterOrderingApplicationLayerServices(
        this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        _ = services.AddMediatR(cfg =>
        {
            _ = cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        _ = services.AddValidatorsFromAssembly(AssemblyReference.Assembly);
        return services;
    }
}
