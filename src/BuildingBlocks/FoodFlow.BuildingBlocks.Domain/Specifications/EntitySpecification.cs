using Ardalis.Specification;
using FoodFlow.BuildingBlocks.Domain.Primitives;

namespace FoodFlow.BuildingBlocks.Domain.Specifications;

/// <summary>
///     Обеспечивает базовую реализацию спецификации, которая может быть применена к сущности.
///     Этот класс поддерживает кастомизацию ключа сущности.
/// </summary>
/// <typeparam name="TEntity">Тип сущности, к которой применяется спецификация.</typeparam>
/// <typeparam name="TKey">Тип ключа, используемого сущностью.</typeparam>
public class EntitySpecification<TEntity, TKey> : Specification<TEntity>
    where TEntity : Entity<TKey>
    where TKey : notnull
{
    /// <summary>
    /// Добавляет условие фильтрации для выборки сущностей по их идентификатору.
    /// </summary>
    /// <param name="key">Идентификатор сущности для фильтрации.</param>
    /// <returns>Экземпляр спецификации <see cref="EntitySpecification{TEntity, TKey}"/> с добавленным условием фильтрации.</returns>
    public EntitySpecification<TEntity, TKey> ByKey(TKey key)
    {
        _ = Query.Where(entity => entity.Id.Equals(key));
        return this;
    }

    // <summary>
    // Добавляет условие фильтрации для выборки сущностей по их идентификатору.
    // </summary>

    /// <param name="keys">Идентификаторы для фильтрации.</param>
    /// <returns>Экземпляр спецификации <see cref="EntitySpecification{TEntity, TKey}"/> с добавленным условием фильтрации.</returns>
    public EntitySpecification<TEntity, TKey> ByKeys(IEnumerable<TKey> keys)
    {
        _ = Query.Where(entity => keys.Contains(entity.Id));
        return this;
    }
}
