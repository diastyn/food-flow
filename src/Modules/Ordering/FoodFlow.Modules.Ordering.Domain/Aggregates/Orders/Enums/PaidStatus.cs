namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Enums;

public sealed class PaidStatus : OrderStatus
{
    public PaidStatus()
        : base(nameof(Paid), 2)
    {
    }
}
