using Ardalis.Specification;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.BuildingBlocks.Domain.Specifications;
using FoodFlow.BuildingBlocks.Infrastructure.Persistence.Extensions;
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

    public virtual async Task<TModel?> GetByIdAsync<TModel>(
        TKey id,
        CancellationToken cancellationToken = default)
    {
        var specification = new AggregateSpecification<TAggregateRoot, TKey>()
            .ByKey(id);

        var model = await _dbSet
            .AsNoTracking()
            .ApplySpecification<TAggregateRoot, TKey>(specification)
            .ProjectTo<TModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return model;
    }

    public virtual async Task<TAggregateRoot?> GetByIdAsync(
        TKey id,
        CancellationToken cancellationToken = default)
    {
        var specification = new AggregateSpecification<TAggregateRoot, TKey>()
            .ByKey(id);

        var entity = await _dbSet
            .AsNoTracking()
            .ApplySpecification<TAggregateRoot, TKey>(specification)
            .FirstOrDefaultAsync(cancellationToken);

        return entity;
    }

    public virtual async Task<TAggregateRoot?> GetAsync(
        ISpecification<TAggregateRoot> specification,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(specification);

        return await _dbSet
            .AsNoTracking()
            .ApplySpecification<TAggregateRoot, TKey>(specification)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TModel>> GetManyAsync<TModel>(
        ISpecification<TAggregateRoot> specification,
        OffsetPagination pagination,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(specification);
        ArgumentNullException.ThrowIfNull(pagination);

        return await _dbSet
            .AsNoTracking()
            .ApplySpecification<TAggregateRoot, TKey>(specification)
            .ApplyPagination<TAggregateRoot, TKey>(pagination)
            .ProjectTo<TModel>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<bool> ExistsAsync(
        ISpecification<TAggregateRoot> specification,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(specification);

        return await _dbSet
            .ApplySpecification<TAggregateRoot, TKey>(specification)
            .AnyAsync(cancellationToken);
    }

    public virtual async Task<TKey> AddAsync(
        TAggregateRoot aggregateRoot,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(aggregateRoot);
        _ = await _dbSet.AddAsync(aggregateRoot, cancellationToken);
        return aggregateRoot.Id;
    }

    public virtual TKey Update(TAggregateRoot aggregateRoot)
    {
        ArgumentNullException.ThrowIfNull(aggregateRoot);
        _ = _dbSet.Update(aggregateRoot);
        return aggregateRoot.Id;
    }
}
