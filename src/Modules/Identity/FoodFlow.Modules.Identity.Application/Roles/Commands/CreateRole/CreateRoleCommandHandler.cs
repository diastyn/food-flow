using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Domain.Aggregates.Roles;
using FoodFlow.Modules.Identity.Domain.Aggregates.Roles.Specifications;
using FoodFlow.Modules.Identity.Domain.Errors;
using FoodFlow.Modules.Identity.Domain.Stores;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodFlow.Modules.Identity.Application.Roles.Commands.CreateRole;

public sealed class CreateRoleCommandHandler(
    IRoleStore roleStore,
    [FromKeyedServices(nameof(Identity))]
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateRoleCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var byNameSpec = new RoleSpecification()
            .ByName(request.Name);
        var role = await roleStore.GetAsync(byNameSpec, cancellationToken);
        if (role is not null)
        {
            return Result.Failure<Guid>(AppErrors.Application.RoleAlreadyExists.New());
        }

        role = Role.Create(request.Name, request.Description);

        _ = await roleStore.AddAsync(role, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(role.Id.Value);
    }
}
