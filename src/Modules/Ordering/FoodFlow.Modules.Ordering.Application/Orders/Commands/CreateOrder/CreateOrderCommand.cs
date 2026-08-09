using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Contracts;
using MediatR;

namespace FoodFlow.Modules.Ordering.Application.Orders.Commands.CreateOrder;

public sealed record CreateOrderCommand(
    Guid CustomerId,
    Guid RestaurantId,
    AddressModel DeliveryAddress,
    List<OrderItemModel> Items) : IRequest<Result<Guid>>;
