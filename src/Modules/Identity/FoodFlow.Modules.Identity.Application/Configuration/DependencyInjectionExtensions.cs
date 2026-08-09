using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Modules.Identity.Application.Configuration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection RegisterIdentityApplicationLayerServices(
        this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        _ = services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(AssemblyReference.Assembly));

        _ = services.AddValidatorsFromAssembly(AssemblyReference.Assembly);

        return services;
    }
}
