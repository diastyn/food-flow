namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Enums;

public sealed class DeliveredStatus : OrderStatus
{
    public DeliveredStatus()
        : base("Delivered", 7)
    {
    }
}
