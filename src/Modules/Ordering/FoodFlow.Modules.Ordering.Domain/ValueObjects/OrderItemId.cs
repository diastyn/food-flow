namespace FoodFlow.Modules.Ordering.Domain.ValueObjects;

public readonly record struct OrderItemId(Guid Value)
{
    public static OrderItemId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();

    public static implicit operator Guid(OrderItemId id) => id.Value;

    public static explicit operator OrderItemId(Guid value) => new(value);
}
