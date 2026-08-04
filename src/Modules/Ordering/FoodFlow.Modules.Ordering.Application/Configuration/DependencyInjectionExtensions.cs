using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Modules.Ordering.Application.Configuration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection RegisterApplicationLayerServices(
        this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });
        
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly);
        return services;
    }
}