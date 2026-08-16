using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using FoodFlow.BuildingBlocks.Domain.Primitives;

namespace FoodFlow.BuildingBlocks.Infrastructure.Persistence.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TAggregateRoot> ApplySpecification<TAggregateRoot, TKey>(
        this IQueryable<TAggregateRoot> query,
        ISpecification<TAggregateRoot>? specification)
        where TAggregateRoot : Entity<TKey>
        where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(query);

        if (specification is null)
        {
            return query;
        }

        return SpecificationEvaluator.Default.GetQuery(
            query,
            specification);
    }

    public static IQueryable<TAggregateRoot> ApplyPagination<TAggregateRoot, TKey>(
        this IQueryable<TAggregateRoot> query,
        OffsetPagination pagination)
        where TAggregateRoot : Entity<TKey>
        where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(pagination);

        return query
            .Skip(Math.Max(pagination.Page - 1, 0) * pagination.PageSize)
            .Take(pagination.PageSize);
    }
}
