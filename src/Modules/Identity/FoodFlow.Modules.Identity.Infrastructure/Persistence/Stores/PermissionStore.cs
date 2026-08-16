using Ardalis.Specification;
using FoodFlow.BuildingBlocks.Infrastructure.Persistence.Extensions;
using FoodFlow.Modules.Identity.Domain.Entities.Permissions;
using FoodFlow.Modules.Identity.Domain.Stores;
using FoodFlow.Modules.Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FoodFlow.Modules.Identity.Infrastructure.Persistence.Stores;

internal sealed class PermissionStore(
    IdentityDbContext dbContext) : IPermissionStore
{
    public async Task<Permission?> GetAsync(
        ISpecification<Permission> specification,
        CancellationToken cancellationToken)
    {
        var permission = await dbContext.Permissions
            .AsNoTracking()
            .ApplySpecification<Permission, PermissionId>(specification)
            .FirstOrDefaultAsync(cancellationToken);
        return permission;
    }

    public async Task<List<Permission>> GetManyAsync(
        ISpecification<Permission> specification,
        CancellationToken cancellationToken)
    {
        var permissions = await dbContext.Permissions
            .AsNoTracking()
            .ApplySpecification<Permission, PermissionId>(specification)
            .ToListAsync(cancellationToken);
        return permissions;
    }
}
