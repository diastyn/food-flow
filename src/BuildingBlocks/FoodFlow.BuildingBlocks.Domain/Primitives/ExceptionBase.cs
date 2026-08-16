using FoodFlow.BuildingBlocks.Results;

namespace FoodFlow.BuildingBlocks.Domain.Primitives;

public abstract class ExceptionBase : Exception
{
    public ExceptionBase(
        Error error,
        Exception? innerException = null)
        : base(GetMessage(error), innerException)
    {
        Error = error;
    }

    public Error Error { get; }

    public string Code => Error.Code;

    public ErrorType ErrorType => Error.Type;

    public string ApplicationCode => Error.ApplicationCode;

    private static string GetMessage(Error error)
    {
        if (error == Error.None)
        {
            throw new ArgumentException("Cannot create an empty exception.", nameof(error));
        }

        if (error.Messages.Length == 0)
        {
            throw new ArgumentException("Error must contain at least one message.", nameof(error));
        }

        return error.Messages[0];
    }
}
