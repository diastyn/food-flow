namespace FoodFlow.Modules.Ordering.Domain.ValueObjects;

public readonly record struct ProductId(Guid Value)
{
    public static ProductId New() => new(Guid.NewGuid());
    
    public override string ToString() => Value.ToString();
    
    public static implicit operator Guid(ProductId value) => value.Value;
    public static explicit operator ProductId(Guid value) => new(value);
}