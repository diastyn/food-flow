using FoodFlow.BuildingBlocks.Application.Commands;
using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Contracts;

namespace FoodFlow.Modules.Ordering.Application.Orders.Commands.CreateOrder;

public sealed record CreateOrderCommand(
    Guid CustomerId,
    Guid RestaurantId,
    AddressModel DeliveryAddress,
    List<OrderItemModel> Items) : ICommandRequest<Result<Guid>>;
