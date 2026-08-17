using FoodFlow.BuildingBlocks.Domain.Security;
using FoodFlow.BuildingBlocks.Security;
using Microsoft.Extensions.Configuration;
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
