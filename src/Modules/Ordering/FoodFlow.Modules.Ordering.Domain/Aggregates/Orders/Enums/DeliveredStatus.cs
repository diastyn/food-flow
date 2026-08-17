namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Enums;

public sealed class DeliveredStatus : OrderStatus
{
    public DeliveredStatus()
        : base(nameof(Delivered), 7)
    {
    }
}
