using Ardalis.SmartEnum;

namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Enums;

public abstract class OrderStatus(string name, int value) : SmartEnum<OrderStatus>(name, value)
{
    public static readonly OrderStatus Placed = new PlacedStatus();

    public static readonly OrderStatus Paid = new PaidStatus();

    public static readonly OrderStatus AcceptedByRestaurant = new AcceptedByRestaurantStatus();

    public static readonly OrderStatus Processing = new ProcessingStatus();

    public static readonly OrderStatus ReadyForPickup = new ReadyForPickupStatus();
    
    public static readonly OrderStatus InTransit = new InTransitStatus();
    
    public static readonly OrderStatus Delivered = new DeliveredStatus();
    
    public static readonly OrderStatus Cancelled = new CancelledStatus();
}