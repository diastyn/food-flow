namespace FoodFlow.Modules.Ordering.Domain.ValueObjects;

public readonly record struct CustomerId(Guid Value)
{
    public static CustomerId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();

    public static implicit operator Guid(CustomerId id) => id.Value;

    public static explicit operator CustomerId(Guid value) => new(value);
}
