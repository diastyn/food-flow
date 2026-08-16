using FoodFlow.BuildingBlocks.Results;
using MediatR;

namespace FoodFlow.Modules.Identity.Application.Roles.Commands.GrantPermission;

public sealed record GrantPermissionsCommand(
    Guid RoleId,
    List<string> Permission) : IRequest<Result>;
