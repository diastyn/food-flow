using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Contracts;
using MediatR;

namespace FoodFlow.Modules.Ordering.Application.Orders.Queries.GetOrders;

public sealed record GetOrdersQuery(
    Guid? RestaurantId,
    int Page = 1,
    int PageSize = 10)
    : OffsetPagination(Page, PageSize),
        IRequest<Result<IReadOnlyList<OrderModel>>>;
