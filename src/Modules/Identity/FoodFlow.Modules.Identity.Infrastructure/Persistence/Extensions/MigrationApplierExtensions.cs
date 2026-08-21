using FoodFlow.BuildingBlocks.Authorization;
using FoodFlow.Modules.Identity.Domain.Aggregates.Roles;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users;
using FoodFlow.Modules.Identity.Domain.Entities.Permissions;
using FoodFlow.Modules.Identity.Domain.Security;
using FoodFlow.Modules.Identity.Domain.ValueObjects;
using FoodFlow.Modules.Identity.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FoodFlow.Modules.Identity.Infrastructure.Persistence.Extensions;

public static class MigrationApplierExtensions
{
    public static async Task MigrateIdentityDatabaseAsync(
        this WebApplication app,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(app);

        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
        var credentials = scope.ServiceProvider.GetRequiredService<IOptions<AdminCredentialsOptions>>().Value;
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        await dbContext.Database.MigrateAsync(cancellationToken);
        await SeedPermissionsAndRolesAsync(dbContext, cancellationToken);
        await SeedAdminAsync(dbContext, credentials, passwordHasher, cancellationToken);
    }

    private static async Task SeedPermissionsAndRolesAsync(
        IdentityDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var permissions = await dbContext.Permissions.ToListAsync(cancellationToken);
        foreach (var permission in AppPermissions.GetAll())
        {
            if (permissions.Exists(p => p.Name == permission))
            {
                continue;
            }

            var newPermission = Permission.Create(permission);
            _ = dbContext.Permissions.Add(newPermission);
            permissions.Add(newPermission);
        }

        _ = await dbContext.SaveChangesAsync(cancellationToken);

        var systemRoles = Enum.GetValues<SystemRole>();
        foreach (var systemRole in systemRoles)
        {
            var roleName = systemRole.ToString().ToUpperInvariant();
            var exists = await dbContext.Roles.AnyAsync(r => r.Name == roleName, cancellationToken);
            if (exists)
            {
                continue;
            }

            var role = Role.FromSystemRole(systemRole);
            role.GrantPermissions(PermissionsFor(systemRole, permissions));
            _ = dbContext.Roles.Add(role);
        }

        _ = await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedAdminAsync(
        IdentityDbContext dbContext,
        AdminCredentialsOptions credentials,
        IPasswordHasher passwordHasher,
        CancellationToken cancellationToken)
    {
        var normalizedUsername = credentials.Username.Trim().ToLowerInvariant();
        if (await dbContext.Users.AnyAsync(r => r.Username == normalizedUsername, cancellationToken))
        {
            return;
        }

        var admin = User.Register(
            Username.Create(credentials.Username),
            Email.Create(credentials.Email),
            PasswordHash.FromHash(passwordHasher.Hash(credentials.Password)),
            PersonName.Create(credentials.Firstname, credentials.Lastname));

        var adminRole = nameof(SystemRole.Admin).ToUpperInvariant();

        var role = await dbContext.Roles.FirstAsync(r => r.Name == adminRole, cancellationToken);

        admin.AssignRole(role);

        _ = await dbContext.Users.AddAsync(admin, cancellationToken);
        _ = await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static IEnumerable<Permission> PermissionsFor(
        SystemRole systemRole,
        List<Permission> permissions)
    {
        ArgumentNullException.ThrowIfNull(permissions);

        return systemRole switch
        {
            SystemRole.Admin => permissions
                .Where(p => AppPermissions
                    .GetAll()
                    .Contains(p.Name)),
            _ => []
        };
    }
}
