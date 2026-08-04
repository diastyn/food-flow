using FoodFlow.BuildingBlocks.Results;

namespace FoodFlow.Modules.Ordering.Application.Orders.Errors;

internal static class OrderErrors
{
    public static class Application
    {
        public static class ValidationError
        {
            public const string NumericCode = "ERR-APP-000001";
            public const string Code = "Create.Order.Validation.Error";
            public const ErrorType Type = ErrorType.Validation;

            public static Error New(string message)
            {
                ArgumentException.ThrowIfNullOrEmpty(nameof(message));
                return new Error(Type, Code, NumericCode, message);
            }
        }
    }
}