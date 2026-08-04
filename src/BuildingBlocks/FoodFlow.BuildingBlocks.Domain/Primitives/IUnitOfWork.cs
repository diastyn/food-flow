namespace FoodFlow.BuildingBlocks.Domain.Primitives;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}