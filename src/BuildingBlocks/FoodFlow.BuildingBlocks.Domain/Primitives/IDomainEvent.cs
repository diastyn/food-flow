namespace FoodFlow.BuildingBlocks.Domain.Primitives;

public interface IDomainEvent
{
    DateTimeOffset OccurredOn { get; }
}