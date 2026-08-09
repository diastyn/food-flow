namespace FoodFlow.Modules.Identity.Domain.ValueObjects;

public readonly record struct UserId(Guid Value)
{
    public static UserId New() => new(Guid.NewGuid());

    public static implicit operator Guid(UserId value) => value.Value;

    public static explicit operator UserId(Guid value) => new(value);

    public override string ToString() => Value.ToString();
}
