using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Domain.Aggregates.Roles.Contracts;
using MediatR;

namespace FoodFlow.Modules.Identity.Application.Roles.Queries.GetRole;

public sealed record GetRoleQuery(
    Guid RoleId) : IRequest<Result<RoleModel>>;
