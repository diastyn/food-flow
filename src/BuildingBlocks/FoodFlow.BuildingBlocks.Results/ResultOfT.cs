namespace FoodFlow.BuildingBlocks.Results;

public class Result<TValue> : Result
{
    protected internal Result(
        bool isSuccess,
        Error error,
        TValue? value)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public TValue Value => IsSuccess
        ? field!
        : throw new InvalidOperationException("The value of a failed result cannot be accessed.");

    public static implicit operator Result<TValue>(TValue value) => Success(value);

    public static implicit operator Result<TValue>(Error error) => Failure<TValue>(error);
}
