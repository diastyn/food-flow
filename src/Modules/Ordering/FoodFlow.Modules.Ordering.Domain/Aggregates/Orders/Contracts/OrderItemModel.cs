namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Contracts;

public sealed class OrderItemModel
{
    public Guid ProductId { get; init; }

    public string ProductName { get; init; } = null!;

    public decimal UnitPrice { get; init; }

    public int Quantity { get; init; }
}
