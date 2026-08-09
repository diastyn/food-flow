using FoodFlow.BuildingBlocks.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace FoodFlow.BuildingBlocks.Infrastructure.Persistence;

public class UnitOfWork<TDbContext>(TDbContext dbContext)
    : IUnitOfWork
    where TDbContext : DbContext
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken) => _ = await dbContext.SaveChangesAsync(cancellationToken);
}
