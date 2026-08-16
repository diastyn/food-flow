using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Enums;

namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Contracts;

public sealed class OrderModel
{
    public Guid Id { get; init; }

    public Guid RestaurantId { get; init; }

    public Guid CustomerId { get; init; }

    public DateTimeOffset CreatedAt { get; init; }

    public OrderStatus Status { get; init; }

    public decimal TotalPrice { get; init; }

    public string Currency { get; init; } = null!;

    public AddressModel DeliveryAddress { get; init; } = null!;

    public IEnumerable<OrderItemModel> OrderItems { get; init; } = [];
}
