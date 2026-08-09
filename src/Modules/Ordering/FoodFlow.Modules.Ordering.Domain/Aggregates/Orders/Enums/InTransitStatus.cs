namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Enums;

public sealed class InTransitStatus : OrderStatus
{
    public InTransitStatus()
        : base(nameof(InTransit), 6)
    {
    }
}
