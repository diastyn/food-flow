namespace FoodFlow.BuildingBlocks.Domain.Primitives;

/// <summary>
/// Базовый класс для корней агрегатов (Aggregate Root).
/// Хранит коллекцию доменных событий, поднятых в рамках одной транзакции,
/// чтобы инфраструктура могла опубликовать их после сохранения агрегата.
/// </summary>
/// <typeparam name="TId">Тип идентификатора агрегата.</typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>, IHasDomainEvents
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected AggregateRoot()
    {
    }

    protected AggregateRoot(TId id)
        : base(id)
    {
    }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}
