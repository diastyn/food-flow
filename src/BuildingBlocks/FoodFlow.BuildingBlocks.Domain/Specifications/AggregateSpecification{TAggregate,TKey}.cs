using FoodFlow.BuildingBlocks.Domain.Primitives;

namespace FoodFlow.BuildingBlocks.Domain.Specifications;

/// <summary>
///     Базовая спецификация для деклораций общих функций.
/// </summary>
/// <typeparam name="TAggregateRoot">Тип аггрегата.</typeparam>
/// <typeparam name="TKey">Тип идентификатора аггрегата.</typeparam>
public class AggregateSpecification<TAggregateRoot, TKey> : EntitySpecification<TAggregateRoot, TKey>
    where TAggregateRoot : Entity<TKey>
    where TKey : notnull;
