using FoodFlow.BuildingBlocks.Application.Queries;
using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Contracts;

namespace FoodFlow.Modules.Ordering.Application.Orders.Queries.GetOrders;

public sealed record GetOrdersQuery(
    Guid? RestaurantId,
    int Page = 1,
    int PageSize = 10)
    : OffsetPagination(Page, PageSize),
        IQueryRequest<Result<IReadOnlyList<OrderModel>>>;
