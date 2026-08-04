namespace FoodFlow.Modules.Ordering.Domain.ValueObjects;

public readonly record struct RestaurantId(Guid Value)
{
    public static RestaurantId New() => new(Guid.NewGuid());
    
    public override string ToString() => Value.ToString();
    
    public static implicit operator Guid(RestaurantId id) => id.Value;
    public static explicit operator RestaurantId(Guid value) => new(value);
}