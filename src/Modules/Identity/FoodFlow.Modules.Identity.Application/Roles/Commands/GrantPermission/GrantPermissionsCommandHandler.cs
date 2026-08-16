using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Domain.Aggregates.Roles.Specifications;
using FoodFlow.Modules.Identity.Domain.Entities.Permissions.Specifications;
using FoodFlow.Modules.Identity.Domain.Errors;
using FoodFlow.Modules.Identity.Domain.Stores;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Modules.Identity.Application.Roles.Commands.GrantPermission;

public sealed class GrantPermissionsCommandHandler(
    IRoleStore roleStore,
    IPermissionStore permissionStore,
    [FromKeyedServices(nameof(Identity))]
    IUnitOfWork unitOfWork)
    : IRequestHandler<GrantPermissionsCommand, Result>
{
    public async Task<Result> Handle(
        GrantPermissionsCommand request,
        CancellationToken cancellationToken)
    {
        var byIdSpec = new RoleSpecification()
            .IncludePermissions()
            .EnableTracking()
            .ByKey(request.RoleId);

        var role = await roleStore.GetAsync(
            byIdSpec,
            cancellationToken);
        if (role is null)
        {
            return Result.Failure(AppErrors.Application.RoleNotFound.New());
        }

        var requestedNames = request.Permission
            .Select(p => p.ToLowerInvariant())
            .Distinct()
            .ToList();

        var byNameSpec = new PermissionSpecification()
            .ByNames(requestedNames)
            .EnableTracking();
        var permissions = await permissionStore.GetManyAsync(
            byNameSpec,
            cancellationToken);

        if (permissions.Count != requestedNames.Count)
        {
            return Result.Failure(AppErrors.Application.PermissionsNotFound.New());
        }

        role.GrantPermissions(permissions);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
