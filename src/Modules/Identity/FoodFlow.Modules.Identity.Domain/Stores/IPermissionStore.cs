using Ardalis.Specification;
using FoodFlow.Modules.Identity.Domain.Entities.Permissions;

namespace FoodFlow.Modules.Identity.Domain.Stores;

public interface IPermissionStore
{
    public Task<Permission?> GetAsync(
        ISpecification<Permission> specification,
        CancellationToken cancellationToken);

    public Task<List<Permission>> GetManyAsync(
        ISpecification<Permission> specification,
        CancellationToken cancellationToken);
}
