using FoodFlow.BuildingBlocks.Application.Queries;
using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Domain.Aggregates.Roles.Contracts;

namespace FoodFlow.Modules.Identity.Application.Roles.Queries.GetRoles;

public sealed record GetRolesQuery(
    string Name,
    int Page = 1,
    int PageSize = 10)
    : OffsetPagination(Page, PageSize),
        IQueryRequest<Result<IReadOnlyList<RoleModel>>>;
