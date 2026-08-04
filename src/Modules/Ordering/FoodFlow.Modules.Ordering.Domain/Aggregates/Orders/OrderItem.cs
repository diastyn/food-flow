using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.Modules.Ordering.Domain.ValueObjects;

namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders;

public class OrderItem : Entity<OrderItemId>
{
    public OrderId OrderId { get; private set; }

    public decimal UnitPrice { get; private set; }

    public int Quantity { get; private set; }

    public ProductId ProductId { get; private set; }
    
    public string ProductName { get; private set; }

    private OrderItem(
        OrderItemId id,
        OrderId orderId,
        decimal unitPrice,
        int quantity,
        string productName,
        ProductId productId) : base(id)
    {
        UnitPrice = unitPrice;
        Quantity = quantity;
        OrderId = orderId;
        ProductId = productId;
        ProductName = productName;
    }

    public static OrderItem Create(
        OrderId orderId,
        decimal unitPrice,
        int quantity,
        string productName,
        ProductId productId)
    {
        ArgumentException.ThrowIfNullOrEmpty(productName);

        if (quantity <= 0)
        {
            throw new DomainException("The order item must contain at least one quantity.");
        }
        
        var item = new OrderItem(
            OrderItemId.New(),
            orderId,
            unitPrice,
            quantity,
            productName,
            productId);

        return item;
    }
}