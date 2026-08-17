using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Domain.Aggregates.Roles.Contracts;
using FoodFlow.Modules.Identity.Domain.Errors;
using FoodFlow.Modules.Identity.Domain.Stores;
using MediatR;

namespace FoodFlow.Modules.Identity.Application.Roles.Queries.GetRole;

public sealed class GetRoleQueryHandler(
    IRoleStore roleStore) : IRequestHandler<GetRoleQuery, Result<RoleModel>>
{
    public async Task<Result<RoleModel>> Handle(
        GetRoleQuery request,
        CancellationToken cancellationToken)
    {
        var role = await roleStore.GetByIdAsync<RoleModel>(
            request.RoleId,
            cancellationToken);

        return role is null
            ? Result.Failure<RoleModel>(AppErrors.Application.RoleNotFound.New())
            : Result.Success(role);
    }
}
