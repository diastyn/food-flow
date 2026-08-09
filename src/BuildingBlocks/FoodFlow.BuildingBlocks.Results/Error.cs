namespace FoodFlow.BuildingBlocks.Results;

public readonly record struct Error(
    ErrorType Type,
    string Code,
    string NumericCode,
    string Message)
{
    public static readonly Error None = new(
        ErrorType.None,
        string.Empty,
        string.Empty,
        string.Empty);
}
