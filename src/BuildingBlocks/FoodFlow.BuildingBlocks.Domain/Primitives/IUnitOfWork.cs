namespace FoodFlow.BuildingBlocks.Domain.Primitives;

public interface IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}
