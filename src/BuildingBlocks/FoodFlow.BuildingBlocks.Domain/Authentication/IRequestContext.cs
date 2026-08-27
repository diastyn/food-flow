namespace FoodFlow.BuildingBlocks.Domain.Authentication;

public interface IRequestContext
{
    public Guid? UserId { get; }

    public string? IpAddress { get; }

    public string? UserAgent { get; }
}
