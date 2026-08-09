namespace FoodFlow.Modules.Identity.Domain.ValueObjects;

public readonly record struct PermissionId(Guid Value)
{
    public static PermissionId New() => new(Guid.NewGuid());

    public static implicit operator Guid(PermissionId id) => id.Value;

    public static implicit operator PermissionId(Guid id) => new(id);
}
