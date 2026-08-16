using FoodFlow.BuildingBlocks.Results;
using MediatR;

namespace FoodFlow.Modules.Identity.Application.Roles.Commands.CreateRole;

public sealed record CreateRoleCommand(
    string Name,
    string? Description = null) : IRequest<Result<Guid>>;
