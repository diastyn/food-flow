using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Domain.Aggregates.Roles.Contracts;
using FoodFlow.Modules.Identity.Domain.Aggregates.Roles.Specifications;
using FoodFlow.Modules.Identity.Domain.Stores;
using MediatR;

namespace FoodFlow.Modules.Identity.Application.Roles.Queries.GetRoles;

public sealed class GetRolesQueryHandler(
    IRoleStore roleStore)
    : IRequestHandler<GetRolesQuery, Result<IReadOnlyList<RoleModel>>>
{
    public async Task<Result<IReadOnlyList<RoleModel>>> Handle(
        GetRolesQuery request,
        CancellationToken cancellationToken)
    {
        var specification = new RoleSpecification();
        if (!string.IsNullOrEmpty(request.Name))
        {
            specification = specification.ByName(request.Name);
        }

        var roles = await roleStore.GetManyAsync<RoleModel>(
            specification,
            request,
            cancellationToken);

        return Result.Success(roles);
    }
}
