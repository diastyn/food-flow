using Ardalis.Specification;

namespace FoodFlow.BuildingBlocks.Domain.Primitives;

public interface IStore<TAggregateRoot, TKey>
    where TAggregateRoot : AggregateRoot<TKey>
    where TKey : notnull
{
    /// <summary>
    /// Получает агрегат, по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены операции для поддержки асинхронной отмены.</param>
    /// <returns>Задача, представляющая асинхронную операцию, результатом которой является модель.</returns>
    public Task<TModel?> GetByIdAsync<TModel>(
        TKey id,
        CancellationToken cancellationToken = default);

    public Task<TAggregateRoot?> GetByIdAsync(
        TKey id,
        CancellationToken cancellationToken = default);

    public Task<TAggregateRoot?> GetAsync(
        ISpecification<TAggregateRoot> specification,
        CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<TModel>> GetManyAsync<TModel>(
        ISpecification<TAggregateRoot> specification,
        OffsetPagination pagination,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Проверяет наличие хотя бы одного агрегата, соответствующего спецификации.
    /// </summary>
    /// <param name="specification">Спецификация для фильтрации агрегатов. Если null, будет проверено наличие любого агрегата.</param>
    /// <param name="cancellationToken">Токен отмены операции для поддержки асинхронной отмены.</param>
    /// <returns>Задача, возвращающая true, если существует хотя бы один агрегат, соответствующий спецификации, иначе false.</returns>
    public Task<bool> ExistsAsync(
        ISpecification<TAggregateRoot> specification,
        CancellationToken cancellationToken = default);

    public Task<TKey> AddAsync(
        TAggregateRoot aggregateRoot,
        CancellationToken cancellationToken = default);

    public TKey Update(TAggregateRoot aggregateRoot);
}
