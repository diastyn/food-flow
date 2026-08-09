namespace FoodFlow.BuildingBlocks.Domain.Security;

public interface IRequestContext
{
    public Guid? UserId { get; }
}
