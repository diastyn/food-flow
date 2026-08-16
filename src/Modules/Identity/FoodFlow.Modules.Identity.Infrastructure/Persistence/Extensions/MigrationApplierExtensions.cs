using FoodFlow.BuildingBlocks.Authorization;
using FoodFlow.Modules.Identity.Domain.Aggregates.Roles;
using FoodFlow.Modules.Identity.Domain.Entities.Permissions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
        await dbContext.Database.MigrateAsync(cancellationToken);
        await SeedPermissionsAndRolesAsync(dbContext, cancellationToken);
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
            var roleName = systemRole.ToString();
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
