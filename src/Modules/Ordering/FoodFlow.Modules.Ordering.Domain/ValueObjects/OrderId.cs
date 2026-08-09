namespace FoodFlow.Modules.Ordering.Domain.ValueObjects;

public readonly record struct OrderId(Guid Value)
{
    public static OrderId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();

    public static implicit operator Guid(OrderId id) => id.Value;

    public static explicit operator OrderId(Guid value) => new(value);
}
