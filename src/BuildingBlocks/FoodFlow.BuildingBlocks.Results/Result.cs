namespace FoodFlow.BuildingBlocks.Results;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None:
                throw new InvalidOperationException("A successful result cannot carry an error.");
            case false when error == Error.None:
                throw new InvalidOperationException("A failed result must carry an error.");
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    public bool IsSuccess { get; }

    public Error Error { get; }

    public bool IsFailure => !IsSuccess;

    public static Result<TValue> Success<TValue>(TValue value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return new Result<TValue>(true, Error.None, value);
    }

    public static Result Success() => new(true, Error.None);

    public static Result<TValue> Failure<TValue>(Error error) => new(false, error, default);

    public static Result Failure(Error error) => new(false, error);
}
