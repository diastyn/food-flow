using Ardalis.Specification;
using FoodFlow.BuildingBlocks.Domain.Specifications;
using FoodFlow.Modules.Identity.Domain.ValueObjects;

namespace FoodFlow.Modules.Identity.Domain.Aggregates.Roles.Specifications;

public sealed class RoleSpecification : AggregateSpecification<Role, RoleId>
{
    public RoleSpecification ByName(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        var normalizedName = name.ToUpperInvariant();

        _ = Query.Where(r => r.Name == normalizedName);

        return this;
    }

    public RoleSpecification IncludePermissions()
    {
        _ = Query.Include(r => r.Permissions);

        return this;
    }
}
