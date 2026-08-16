namespace FoodFlow.BuildingBlocks.Domain.Security;

public interface IRequestContext
{
    public Guid? UserId { get; }

    public string? IpAddress { get; }

    public string? UserAgent { get; }
}
