using FoodFlow.BuildingBlocks.Application.Commands;
using FoodFlow.BuildingBlocks.Results;

namespace FoodFlow.Modules.Identity.Application.Roles.Commands.GrantPermission;

public sealed record GrantPermissionsCommand(
    Guid RoleId,
    List<string> Permission) : ICommandRequest<Result>;
