namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Enums;

public sealed class CancelledStatus : OrderStatus
{
    public CancelledStatus()
        : base(nameof(Cancelled), 8)
    {
    }
}
