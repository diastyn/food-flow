using FoodFlow.BuildingBlocks.Application.Queries;
using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Domain.Aggregates.Roles.Contracts;

namespace FoodFlow.Modules.Identity.Application.Roles.Queries.GetRole;

public sealed record GetRoleQuery(Guid RoleId) : IQueryRequest<Result<RoleModel>>;
