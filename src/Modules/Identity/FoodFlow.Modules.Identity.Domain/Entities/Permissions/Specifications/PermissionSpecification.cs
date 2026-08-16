using Ardalis.Specification;
using FoodFlow.BuildingBlocks.Domain.Specifications;
using FoodFlow.Modules.Identity.Domain.ValueObjects;

namespace FoodFlow.Modules.Identity.Domain.Entities.Permissions.Specifications;

public sealed class PermissionSpecification : AggregateSpecification<Permission, PermissionId>
{
    public PermissionSpecification ByName(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        var normalized = name.ToLowerInvariant();

        _ = Query.Where(p => p.Name == normalized);

        return this;
    }

    public PermissionSpecification ByNames(List<string> names)
    {
        ArgumentNullException.ThrowIfNull(names);

        var normalizedNames = names.Select(n => n.ToLowerInvariant());

        _ = Query.Where(p => normalizedNames.Contains(p.Name));

        return this;
    }
}
