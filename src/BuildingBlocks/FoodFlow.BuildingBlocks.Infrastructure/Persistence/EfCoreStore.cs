using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.BuildingBlocks.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace FoodFlow.BuildingBlocks.Infrastructure.Persistence;

public class EfCoreStore<TAggregateRoot, TKey> : IStore<TAggregateRoot, TKey>
    where TAggregateRoot : AggregateRoot<TKey>
    where TKey : notnull
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<TAggregateRoot> _dbSet;
    protected readonly IMapper _mapper;

    public EfCoreStore(DbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TAggregateRoot>();
        _mapper = mapper;
    }

    public virtual async Task<TModel> GetByIdAsync<TModel>(
        TKey id,
        CancellationToken cancellationToken = default)
    {
        var specification = new AggregateSpecification<TAggregateRoot, TKey>()
            .ByKey(id);

        var model = await ApplySpecification(_dbSet, specification)
            .AsNoTracking()
            .ProjectTo<TModel>(_mapper.ConfigurationProvider)
            .SingleAsync(cancellationToken);

        return model;
    }

    public async Task<TAggregateRoot?> GetByIdAsync(
        TKey id,
        CancellationToken cancellationToken = default)
    {
        var specification = new AggregateSpecification<TAggregateRoot, TKey>()
            .ByKey(id);

        var entity = await ApplySpecification(_dbSet, specification)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        return entity;
    }

    public async Task<TAggregateRoot?> GetAsync(
        ISpecification<TAggregateRoot> specification,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(specification);

        return await ApplySpecification(_dbSet, specification)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<bool> ExistsAsync(
        ISpecification<TAggregateRoot> specification,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(specification);

        return await ApplySpecification(_dbSet, specification)
            .AnyAsync(cancellationToken);
    }

    private static IQueryable<TAggregateRoot> ApplySpecification(
        IQueryable<TAggregateRoot> query,
        ISpecification<TAggregateRoot>? specification)
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
}
