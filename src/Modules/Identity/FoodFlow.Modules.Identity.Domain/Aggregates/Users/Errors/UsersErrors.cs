using FoodFlow.BuildingBlocks.Results;

namespace FoodFlow.Modules.Identity.Domain.Aggregates.Users.Errors;

public static class UsersErrors
{
    public static class Application
    {
        public static class ValidationError
        {
            public const string NumericCode = "ERR-APP-000101";
            public const string Code = "Register.User.Validation.Error";
            public const ErrorType Type = ErrorType.Validation;

            public static Error New(string message)
            {
                ArgumentException.ThrowIfNullOrEmpty(nameof(message));
                return new Error(Type, Code, NumericCode, message);
            }
        }

        public static class UserUnauthorized
        {
            public const string NumericCode = "ERR-APP-000102";
            public const string Code = "User.Unauthorized.Error";
            public const ErrorType Type = ErrorType.Unauthorized;
            public const string Message = "Incorrect username or password.";

            public static Error New() => new(Type, Code, NumericCode, Message);
        }

        public static class NotFound
        {
            public const string NumericCode = "ERR-APP-000103";
            public const string Code = "User.NotFound.Error";
            public const ErrorType Type = ErrorType.NotFound;
            public const string Message = "User was not found.";

            public static Error New() => new(Type, Code, NumericCode, Message);
        }
    }
}
