using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Contracts;
using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Enums;
using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Events;
using FoodFlow.Modules.Ordering.Domain.Errors;
using FoodFlow.Modules.Ordering.Domain.ValueObjects;

namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders;

public class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderItem> _items = [];

    public RestaurantId RestaurantId { get; private set; }

    public CustomerId CustomerId { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public Money TotalPrice { get; private set; }

    public OrderStatus Status { get; private set; }

    public Address DeliveryAddress { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items;

    /// <summary>
    /// Ef core constructor.
    /// </summary>
    private Order()
    {
    }

    private Order(
        OrderId id,
        RestaurantId restaurantId,
        CustomerId customerId,
        DateTimeOffset createdAt,
        Money totalPrice,
        OrderStatus status,
        Address deliveryAddress,
        IEnumerable<OrderItem> items)
        : base(id)
    {
        RestaurantId = restaurantId;
        CustomerId = customerId;
        CreatedAt = createdAt;
        TotalPrice = totalPrice;
        Status = status;
        DeliveryAddress = deliveryAddress;
        _items = [.. items];
    }

    public static Order Create(
        RestaurantId restaurantId,
        CustomerId customerId,
        DateTimeOffset createdAt,
        Money totalPrice,
        Address deliveryAddress,
        List<OrderItemModel> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(deliveryAddress);

        if (items.Count == 0)
        {
            throw new DomainException(AppErrors.Domain.OrderMustContainAtLeastOneItem.New());
        }

        var id = OrderId.New();
        var order = new Order(
            id,
            restaurantId,
            customerId,
            createdAt,
            totalPrice,
            OrderStatus.Placed,
            deliveryAddress,
            items.Select(item => OrderItem.Create(
                id,
                item.UnitPrice,
                item.Quantity,
                item.ProductName,
                new ProductId(item.ProductId))));

        order.RaiseDomainEvent(new OrderCreatedDomainEvent(
            restaurantId,
            customerId,
            order.Id,
            order.TotalPrice.Amount,
            order.DeliveryAddress.ToString()));

        return order;
    }
}
