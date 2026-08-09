using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.Modules.Identity.Domain.ValueObjects;

namespace FoodFlow.Modules.Identity.Domain.Entities;

public class Permission : Entity<PermissionId>
{
    private Permission()
    {
    }

    private Permission(
        PermissionId id,
        string name,
        string? description = null)
        : base(id)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public static Permission Create(string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Permission name cannot be empty.");
        }

        return new Permission(PermissionId.New(), name, description);
    }
}
