namespace FoodFlow.BuildingBlocks.Domain.Primitives;

public abstract record DomainEvent : IDomainEvent
{
    public DateTimeOffset OccurredOn => DateTimeOffset.UtcNow;
}