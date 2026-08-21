using FoodFlow.BuildingBlocks.Application.Commands;
using FoodFlow.BuildingBlocks.Results;

namespace FoodFlow.Modules.Identity.Application.Users.Commands.AssignRole;

public sealed record AssignRoleCommand(
    Guid UserId,
    string RoleName) : ICommandRequest<Result>;
