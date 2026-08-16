using FoodFlow.BuildingBlocks.Results;

namespace FoodFlow.BuildingBlocks.Domain.Errors;

public static partial class AppErrors
{
    public static class Application
    {
        public static class Validation
        {
            public const string Code = "Validation.Error";
            public const string ApplicationCode = "ERR-APP-000001";
            public const ErrorType Type = ErrorType.Validation;

            public static Error New(string[] messages)
            {
                ArgumentNullException.ThrowIfNull(messages);
                return new Error(Type, Code, ApplicationCode, messages);
            }
        }
    }
}
