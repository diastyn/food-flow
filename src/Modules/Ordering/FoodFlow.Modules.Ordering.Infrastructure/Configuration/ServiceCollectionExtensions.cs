using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.Modules.Ordering.Domain.Stores;
using FoodFlow.Modules.Ordering.Infrastructure.Persistence;
using FoodFlow.Modules.Ordering.Infrastructure.Persistence.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Modules.Ordering.Infrastructure.Configuration;

public static class ServiceCollectionExtensions
{
    public const string ConnectionStringName = "Ordering";
    
    public static IServiceCollection RegisterInfrastructureLayerServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        string? connectionString = configuration.GetConnectionString(ConnectionStringName);

        services.AddDbContext<OrderingDbContext>(options =>
        {
            options.UseNpgsql(connectionString, 
                npgsql => npgsql
                    .EnableRetryOnFailure()
                    .MigrationsHistoryTable("__EFMigrationsHistory", OrderingDbContext.DefaultSchema));
        });

        services.AddScoped<IOrderStore, OrderStore>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton(TimeProvider.System);
        
        return services;
    }
}