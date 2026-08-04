namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Enums;

public sealed class ProcessingStatus : OrderStatus
{
    public ProcessingStatus() : base(nameof(Processing), 4)
    {
    }
}