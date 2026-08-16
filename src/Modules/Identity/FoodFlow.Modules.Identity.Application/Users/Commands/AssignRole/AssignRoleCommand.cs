using FoodFlow.BuildingBlocks.Results;
using MediatR;

namespace FoodFlow.Modules.Identity.Application.Users.Commands.AssignRole;

public sealed record AssignRoleCommand(
    Guid UserId,
    string RoleName) : IRequest<Result>;
