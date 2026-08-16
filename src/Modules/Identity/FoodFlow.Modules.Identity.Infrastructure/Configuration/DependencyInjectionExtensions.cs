using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.BuildingBlocks.Infrastructure.Persistence;
using FoodFlow.Modules.Identity.Domain.Auth;
using FoodFlow.Modules.Identity.Domain.Security;
using FoodFlow.Modules.Identity.Domain.Stores;
using FoodFlow.Modules.Identity.Infrastructure.Auth;
using FoodFlow.Modules.Identity.Infrastructure.Persistence;
using FoodFlow.Modules.Identity.Infrastructure.Persistence.Stores;
using FoodFlow.Modules.Identity.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Modules.Identity.Infrastructure.Configuration;

public static class DependencyInjectionExtensions
{
    private const string ConnectionStringName = "Database";

    public static IServiceCollection RegisterIdentityInfrastructureLayerServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        var connectionString = configuration.GetConnectionString(ConnectionStringName);

        _ = services.AddDbContext<IdentityDbContext>(options =>
            options.UseNpgsql(connectionString,
                npgsql => npgsql.EnableRetryOnFailure()
                    .MigrationsHistoryTable("__EFMigrationsHistory", IdentityDbContext.DefaultSchema)));

        _ = services
            .AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        _ = services.AddSingleton<RsaSigningKeyProvider>();
        _ = services.AddTransient<IJwtTokenIssuer, JwtTokenIssuer>();
        _ = services.AddTransient<IUserStore, UserStore>();
        _ = services.AddTransient<IRoleStore, RoleStore>();
        _ = services.AddTransient<IPermissionStore, PermissionStore>();
        _ = services.AddTransient<IAuditLogStore, AuditLogStore>();
        _ = services.AddKeyedTransient<IUnitOfWork, UnitOfWork<IdentityDbContext>>(nameof(Identity));
        _ = services.AddTransient<IPasswordHasher, Pbkdf2PasswordHasher>();

        return services;
    }
}
