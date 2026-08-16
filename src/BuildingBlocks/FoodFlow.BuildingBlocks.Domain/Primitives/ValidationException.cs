using FoodFlow.BuildingBlocks.Results;

namespace FoodFlow.BuildingBlocks.Domain.Primitives;

public class ValidationException : ExceptionBase
{
    public ValidationException(
        Error error,
        string[]? properties = null,
        Exception? innerException = null)
        : base(error, innerException)
    {
        Properties = properties ?? Array.Empty<string>();
    }

    public string[]? Properties { get; }
}
