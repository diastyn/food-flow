namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Enums;

public sealed class ReadyForPickupStatus : OrderStatus
{
    public ReadyForPickupStatus()
        : base(nameof(ReadyForPickup), 5)
    {
    }
}
