namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Enums;

public sealed class AcceptedByRestaurantStatus : OrderStatus
{
    public AcceptedByRestaurantStatus() : base(nameof(AcceptedByRestaurant), 3)
    {
    }
}