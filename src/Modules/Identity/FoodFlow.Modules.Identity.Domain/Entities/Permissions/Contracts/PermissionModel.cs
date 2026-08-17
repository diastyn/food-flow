namespace FoodFlow.Modules.Identity.Domain.Entities.Permissions.Contracts;

public sealed class PermissionModel
{
    public Guid Id { get; init; }

    public string Name { get; init; } = null!;

    public string? Description { get; init; }
}
