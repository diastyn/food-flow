namespace FoodFlow.Modules.Identity.Domain.ValueObjects;

public readonly record struct RoleId(Guid Value)
{
    public static RoleId New() => new(Guid.NewGuid());

    public static implicit operator Guid(RoleId roleId) => roleId.Value;

    public static implicit operator RoleId(Guid roleId) => new(roleId);
}
