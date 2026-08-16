using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Domain.Aggregates.Roles.Specifications;
using FoodFlow.Modules.Identity.Domain.Errors;
using FoodFlow.Modules.Identity.Domain.Stores;
using FoodFlow.Modules.Identity.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Modules.Identity.Application.Users.Commands.AssignRole;

public sealed class AssignRoleCommandHandler(
    IUserStore userStore,
    IRoleStore roleStore,
    [FromKeyedServices(nameof(Identity))]
    IUnitOfWork unitOfWork)
    : IRequestHandler<AssignRoleCommand, Result>
{
    public async Task<Result> Handle(
        AssignRoleCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userStore.GetByIdAsync(
            new UserId(request.UserId),
            cancellationToken);

        if (user is null)
        {
            return Result.Failure(AppErrors.Application.UserNotFound.New());
        }

        var byRoleNameSpec = new RoleSpecification()
            .ByName(request.RoleName);

        var role = await roleStore.GetAsync(byRoleNameSpec, cancellationToken);
        if (role is null)
        {
            return Result.Failure(AppErrors.Application.RoleNotFound.New());
        }

        user.AssignRole(role);
        _ = userStore.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
