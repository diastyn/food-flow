namespace FoodFlow.BuildingBlocks.Domain.Primitives;

/// <summary>
/// Базовый класс для сущностей (Entity), идентифицируемых по типу <typeparamref name="TId"/>.
/// Равенство основано на идентичности (по Id), а не на значениях атрибутов.
/// </summary>
/// <typeparam name="TId">Тип идентификатора сущности.</typeparam>
public abstract class Entity<TId> : IEquatable<Entity<TId>> where TId : notnull
{
    /// <summary>
    /// Уникальный идентификатор сущности.
    /// </summary>
    public TId Id { get; protected set; } = default!;

    /// <summary>
    /// Инициализирует сущность с заданным идентификатором.
    /// </summary>
    /// <param name="id">Уникальный идентификатор сущности.</param>
    protected Entity(TId id) => Id = id;

    /// <summary>
    /// Конструктор без параметров — используется EF Core при материализации объекта из базы данных.
    /// </summary>
    protected Entity()
    {
    }

    /// <summary>
    /// Сравнивает текущую сущность с другой по идентификатору и типу.
    /// </summary>
    /// <param name="other">Другая сущность для сравнения.</param>
    /// <returns><c>true</c>, если сущности одного типа и имеют одинаковый идентификатор.</returns>
    public bool Equals(Entity<TId>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (IsTransient || other.IsTransient)
        {
            return false;
        }

        return other.GetType() == GetType() && EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    /// <inheritdoc/>
    public sealed override bool Equals(object? obj)
    {
        return obj is Entity<TId> other && Equals(other);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        if (IsTransient)
        {
            return base.GetHashCode();
        }

        return EqualityComparer<TId>.Default.GetHashCode(Id);
    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right) => Equals(left, right);

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !Equals(left, right);

    /// <summary>
    /// Проверяет, является ли сущность временной (еще не имеющей Id в хранилище).
    /// </summary>
    protected bool IsTransient => EqualityComparer<TId>.Default.Equals(Id, default);
}