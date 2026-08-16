using FoodFlow.BuildingBlocks.Results;

namespace FoodFlow.BuildingBlocks.Domain.Primitives;

public class DomainException : ExceptionBase
{
    public DomainException(
        Error error,
        Exception? innerException = null)
        : base(error, innerException)
    {
    }
}
