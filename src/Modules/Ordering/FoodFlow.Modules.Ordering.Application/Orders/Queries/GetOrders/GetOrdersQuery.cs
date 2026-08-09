using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Contracts;
using MediatR;

namespace FoodFlow.Modules.Ordering.Application.Orders.Queries.GetOrders;

public sealed record GetOrdersQuery(
    Guid RestaurantId) : IRequest<Result<IReadOnlyList<OrderModel>>>;
