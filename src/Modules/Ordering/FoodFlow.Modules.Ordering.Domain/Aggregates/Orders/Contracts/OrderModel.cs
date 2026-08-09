using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Enums;

namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Contracts;

public sealed record OrderModel(
    Guid Id,
    Guid RestaurantId,
    Guid CustomerId,
    DateTimeOffset CreatedAt,
    OrderStatus Status,
    decimal TotalPrice,
    string Currency,
    AddressModel DeliveryAddress,
    IEnumerable<OrderItemModel> OrderItems);
