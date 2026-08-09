namespace FoodFlow.BuildingBlocks.Domain.Primitives;

public interface IDomainEvent
{
    public DateTimeOffset OccurredOn { get; }
}
