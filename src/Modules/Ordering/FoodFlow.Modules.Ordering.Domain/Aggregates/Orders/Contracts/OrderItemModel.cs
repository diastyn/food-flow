namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Contracts;

public sealed record OrderItemModel(
    Guid ProductId,
    string ProductName,
    decimal UnitPrice,
    int Quantity);
