using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.Modules.Identity.Domain.Errors;
using FoodFlow.Modules.Identity.Domain.ValueObjects;

namespace FoodFlow.Modules.Identity.Domain.Entities.Permissions;

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
            throw new DomainException(AppErrors.Domain.PermissionNameCannotBeEmpty.New());
        }

        return new Permission(PermissionId.New(), name, description);
    }
}
