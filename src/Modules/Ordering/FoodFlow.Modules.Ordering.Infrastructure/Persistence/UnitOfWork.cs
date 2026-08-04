using FoodFlow.BuildingBlocks.Domain.Primitives;

namespace FoodFlow.Modules.Ordering.Infrastructure.Persistence;

internal sealed class UnitOfWork(OrderingDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}