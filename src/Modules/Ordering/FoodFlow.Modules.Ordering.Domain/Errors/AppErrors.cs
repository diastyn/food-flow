using FoodFlow.BuildingBlocks.Results;

namespace FoodFlow.Modules.Ordering.Domain.Errors;

public static partial class AppErrors
{
    public static class Application
    {
        public static class ValidationError
        {
            public const string ApplicationCode = "ERR-ORDERING-000001";
            public const string Code = "Create.Order.Validation.Error";
            public const ErrorType Type = ErrorType.Validation;

            public static Error New(string message)
            {
                ArgumentException.ThrowIfNullOrEmpty(message);
                return new Error(Type, Code, ApplicationCode, [message]);
            }
        }
    }

    public static class Domain
    {
        public static class CountryCannotBeEmpty
        {
            public const string ApplicationCode = "ERR-ORDERING-000002";
            public const string Code = "Address.Country.IsEmpty.Error";
            public const ErrorType Type = ErrorType.Validation;
            public const string Message = "Country cannot be empty.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class CityCannotBeEmpty
        {
            public const string ApplicationCode = "ERR-ORDERING-000003";
            public const string Code = "Address.City.IsEmpty.Error";
            public const ErrorType Type = ErrorType.Validation;
            public const string Message = "City cannot be empty.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class StreetCannotBeEmpty
        {
            public const string ApplicationCode = "ERR-ORDERING-000004";
            public const string Code = "Address.Street.IsEmpty.Error";
            public const ErrorType Type = ErrorType.Validation;
            public const string Message = "Street cannot be empty.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class CurrencyCodeCannotBeEmpty
        {
            public const string ApplicationCode = "ERR-ORDERING-000005";
            public const string Code = "Currency.Code.IsEmpty.Error";
            public const ErrorType Type = ErrorType.Validation;
            public const string Message = "Currency code cannot be null or whitespace.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class CurrencyDecimalPlacesOutOfRange
        {
            public const string ApplicationCode = "ERR-ORDERING-000006";
            public const string Code = "Currency.DecimalPlaces.OutOfRange.Error";
            public const ErrorType Type = ErrorType.Validation;
            public const string Message = "Decimal places must be between 0 and 4.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class CurrencyCodeIsUnsupported
        {
            public const string ApplicationCode = "ERR-ORDERING-000007";
            public const string Code = "Currency.Code.IsUnsupported.Error";
            public const ErrorType Type = ErrorType.Validation;

            public static Error New(string code)
            {
                ArgumentException.ThrowIfNullOrEmpty(code);
                var message = $"Unsupported currency code: '{code}'.";
                return new(Type, Code, ApplicationCode, [message]);
            }
        }

        public static class MoneyCurrencyMismatch
        {
            public const string ApplicationCode = "ERR-ORDERING-000008";
            public const string Code = "Money.Currency.Mismatch.Error";
            public const ErrorType Type = ErrorType.Validation;
            public const string Message = "Cannot operate on money with different or uninitialized currencies.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class OrderItemQuantityMustBePositive
        {
            public const string ApplicationCode = "ERR-ORDERING-000009";
            public const string Code = "OrderItem.Quantity.MustBePositive.Error";
            public const ErrorType Type = ErrorType.Validation;
            public const string Message = "The order item must contain at least one quantity.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class OrderMustContainAtLeastOneItem
        {
            public const string ApplicationCode = "ERR-ORDERING-000010";
            public const string Code = "Order.Items.MustContainAtLeastOne.Error";
            public const ErrorType Type = ErrorType.Validation;
            public const string Message = "The order must contain at least one item.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }
    }
}
