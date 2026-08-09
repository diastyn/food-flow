using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.BuildingBlocks.Infrastructure.Persistence;
using FoodFlow.Modules.Ordering.Domain.Stores;
using FoodFlow.Modules.Ordering.Infrastructure.Persistence;
using FoodFlow.Modules.Ordering.Infrastructure.Persistence.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Modules.Ordering.Infrastructure.Configuration;

public static class ServiceCollectionExtensions
{
    public const string ConnectionStringName = "Database";

    public static IServiceCollection RegisterOrderingInfrastructureLayerServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        var connectionString = configuration.GetConnectionString(ConnectionStringName);

        _ = services.AddDbContext<OrderingDbContext>(options =>
        {
            _ = options.UseNpgsql(connectionString,
                npgsql => npgsql
                    .EnableRetryOnFailure()
                    .MigrationsHistoryTable("__EFMigrationsHistory", OrderingDbContext.DefaultSchema));
        });

        _ = services.AddScoped<IOrderStore, OrderStore>();
        _ = services.AddKeyedScoped<IUnitOfWork, UnitOfWork<OrderingDbContext>>(nameof(Ordering));
        _ = services.AddSingleton(TimeProvider.System);

        return services;
    }
}
