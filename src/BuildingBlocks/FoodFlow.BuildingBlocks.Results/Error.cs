namespace FoodFlow.BuildingBlocks.Results;

public readonly record struct Error(
    ErrorType Type,
    string Code,
    string ApplicationCode,
    string[] Messages)
{
    public static readonly Error None = new(
        ErrorType.None,
        string.Empty,
        string.Empty,
        []);
}
