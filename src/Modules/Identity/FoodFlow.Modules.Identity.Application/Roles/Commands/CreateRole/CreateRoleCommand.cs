using FoodFlow.BuildingBlocks.Application.Commands;
using FoodFlow.BuildingBlocks.Results;

namespace FoodFlow.Modules.Identity.Application.Roles.Commands.CreateRole;

public sealed record CreateRoleCommand(
    string Name,
    string? Description = null) : ICommandRequest<Result<Guid>>;
