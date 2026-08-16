using FoodFlow.BuildingBlocks.Authorization;
using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.Modules.Identity.Domain.Entities.Permissions;
using FoodFlow.Modules.Identity.Domain.Errors;
using FoodFlow.Modules.Identity.Domain.ValueObjects;

namespace FoodFlow.Modules.Identity.Domain.Aggregates.Roles;

public sealed class Role : AggregateRoot<RoleId>
{
    private readonly List<Permission> _permissions = [];

    private Role()
    {
    }

    private Role(
        RoleId id,
        string name,
        string? description = null)
        : base(id)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public IReadOnlyCollection<Permission> Permissions => _permissions;

    public static Role Create(string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException(AppErrors.Domain.RoleNameCannotBeEmpty.New());
        }

        var normalizedName = name.ToUpperInvariant();
        return new Role(RoleId.New(), normalizedName, description);
    }

    public void GrantPermissions(params IEnumerable<Permission> permissions)
    {
        ArgumentNullException.ThrowIfNull(permissions);

        foreach (var permission in permissions)
        {
            if (_permissions.Exists(p => p.Id == permission.Id))
            {
                continue;
            }

            _permissions.Add(permission);
        }
    }

    public void RevokePermissions(params Permission[] permissions)
    {
        ArgumentNullException.ThrowIfNull(permissions);

        foreach (var permission in permissions)
        {
            _ = _permissions.RemoveAll(p => p.Id == permission.Id);
        }
    }

    public static Role FromSystemRole(SystemRole role) =>
        Create(role.ToString(), $"Built-in {role} role.");
}
