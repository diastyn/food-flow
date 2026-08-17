using FoodFlow.Modules.Identity.Domain.Entities.Permissions.Contracts;

namespace FoodFlow.Modules.Identity.Domain.Aggregates.Roles.Contracts;

public sealed class RoleModel
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public string? Description { get; init; }

    public IReadOnlyList<PermissionModel> Permissions { get; init; } = [];
}
