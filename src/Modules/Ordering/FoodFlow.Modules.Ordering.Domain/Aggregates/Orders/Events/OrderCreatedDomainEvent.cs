using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.Modules.Ordering.Domain.ValueObjects;

namespace FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Events;

public sealed record OrderCreatedDomainEvent(
    RestaurantId restaurantId,
    CustomerId CustomerId,
    OrderId OrderId,
    decimal TotalPrice,
    string DeliveryAddress) : DomainEvent;
