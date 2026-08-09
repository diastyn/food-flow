namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Enums;

public sealed class PlacedStatus : OrderStatus
{
    public PlacedStatus()
        : base(nameof(Placed), 1)
    {
    }
}
