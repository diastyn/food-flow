using FoodFlow.BuildingBlocks.Results;

namespace FoodFlow.Modules.Identity.Domain.Errors;

public static partial class AppErrors
{
    public static class Application
    {
        public static class ValidationError
        {
            public const string ApplicationCode = "ERR-IDENTITY-000001";
            public const string Code = "Register.User.Validation.Error";
            public const ErrorType Type = ErrorType.Validation;

            public static Error New(string message)
            {
                ArgumentException.ThrowIfNullOrEmpty(message);
                return new Error(Type, Code, ApplicationCode, [message]);
            }
        }

        public static class UserUnauthorized
        {
            public const string ApplicationCode = "ERR-IDENTITY-000002";
            public const string Code = "User.Unauthorized.Error";
            public const ErrorType Type = ErrorType.Unauthorized;
            public const string Message = "Incorrect username or password.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class UserNotFound
        {
            public const string ApplicationCode = "ERR-IDENTITY-000003";
            public const string Code = "User.NotFound.Error";
            public const ErrorType Type = ErrorType.NotFound;
            public const string Message = "User was not found.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class ExistsByUsername
        {
            public const string ApplicationCode = "ERR-IDENTITY-000004";
            public const string Code = "Exists.ByUsername.Error";
            public const ErrorType Type = ErrorType.Conflict;
            public const string Message = "This username already exists.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class ExistsByEmail
        {
            public const string ApplicationCode = "ERR-IDENTITY-000005";
            public const string Code = "Exists.ByEmail.Error";
            public const ErrorType Type = ErrorType.Conflict;
            public const string Message = "This email already exists.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class RoleNotFound
        {
            public const string ApplicationCode = "ERR-IDENTITY-000006";
            public const string Code = "Role.NotFound.Error";
            public const ErrorType Type = ErrorType.NotFound;
            public const string Message = "Role was not found.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class PermissionNotFound
        {
            public const string ApplicationCode = "ERR-IDENTITY-000007";
            public const string Code = "Permission.NotFound.Error";
            public const ErrorType Type = ErrorType.NotFound;
            public const string Message = "Permission was not found.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class PermissionsNotFound
        {
            public const string ApplicationCode = "ERR-IDENTITY-000008";
            public const string Code = "Permissions.NotFound.Error";
            public const ErrorType Type = ErrorType.NotFound;
            public const string Message = "Permissions were not found.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class RoleAlreadyExists
        {
            public const string ApplicationCode = "ERR-IDENTITY-000021";
            public const string Code = "Role.AlreadyExists.Error";
            public const ErrorType Type = ErrorType.Conflict;
            public const string Message = "A role with this name already exists.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }
    }

    public static class Domain
    {
        public static class UserAccountIsDeactivated
        {
            public const string ApplicationCode = "ERR-IDENTITY-000009";
            public const string Code = "User.IsDeactivated.Error";
            public const ErrorType Type = ErrorType.Conflict;
            public const string Message = "Cannot sign in: the user account is deactivated.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class RoleNameCannotBeEmpty
        {
            public const string ApplicationCode = "ERR-IDENTITY-000010";
            public const string Code = "Role.Name.IsEmpty.Error";
            public const ErrorType Type = ErrorType.Validation;
            public const string Message = "Role name cannot be empty.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class PermissionNameCannotBeEmpty
        {
            public const string ApplicationCode = "ERR-IDENTITY-000011";
            public const string Code = "Permission.Name.IsEmpty.Error";
            public const ErrorType Type = ErrorType.Validation;
            public const string Message = "Permission name cannot be empty.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class EmailCannotBeEmpty
        {
            public const string ApplicationCode = "ERR-IDENTITY-000012";
            public const string Code = "Email.IsEmpty.Error";
            public const ErrorType Type = ErrorType.Validation;
            public const string Message = "Email cannot be empty.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class EmailIsNotValid
        {
            public const string ApplicationCode = "ERR-IDENTITY-000013";
            public const string Code = "Email.IsNotValid.Error";
            public const ErrorType Type = ErrorType.Validation;

            public static Error New(string email)
            {
                ArgumentException.ThrowIfNullOrEmpty(email);
                var message = $"'{email}' is not a valid email address.";
                return new(Type, Code, ApplicationCode, [message]);
            }
        }

        public static class PasswordHashCannotBeEmpty
        {
            public const string ApplicationCode = "ERR-IDENTITY-000014";
            public const string Code = "PasswordHash.IsEmpty.Error";
            public const ErrorType Type = ErrorType.Validation;
            public const string Message = "Password hash cannot be empty.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class FirstNameCannotBeEmpty
        {
            public const string ApplicationCode = "ERR-IDENTITY-000015";
            public const string Code = "PersonName.FirstName.IsEmpty.Error";
            public const ErrorType Type = ErrorType.Validation;
            public const string Message = "First name cannot be empty.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class LastNameCannotBeEmpty
        {
            public const string ApplicationCode = "ERR-IDENTITY-000016";
            public const string Code = "PersonName.LastName.IsEmpty.Error";
            public const ErrorType Type = ErrorType.Validation;
            public const string Message = "Last name cannot be empty.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class PhoneNumberIsRequired
        {
            public const string ApplicationCode = "ERR-IDENTITY-000017";
            public const string Code = "PhoneNumber.IsRequired.Error";
            public const ErrorType Type = ErrorType.Validation;
            public const string Message = "Phone number is required.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class PhoneNumberIsInvalid
        {
            public const string ApplicationCode = "ERR-IDENTITY-000018";
            public const string Code = "PhoneNumber.IsInvalid.Error";
            public const ErrorType Type = ErrorType.Validation;
            public const string Message = "Invalid Kazakhstan phone number.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class UsernameCannotBeEmpty
        {
            public const string ApplicationCode = "ERR-IDENTITY-000019";
            public const string Code = "Username.IsEmpty.Error";
            public const ErrorType Type = ErrorType.Validation;
            public const string Message = "Username cannot be empty.";

            public static Error New() => new(Type, Code, ApplicationCode, [Message]);
        }

        public static class UsernameLengthIsInvalid
        {
            public const string ApplicationCode = "ERR-IDENTITY-000020";
            public const string Code = "Username.LengthIsInvalid.Error";
            public const ErrorType Type = ErrorType.Validation;

            public static Error New(int minLength, int maxLength)
            {
                var message = $"Username must be between {minLength} and {maxLength} characters.";
                return new(Type, Code, ApplicationCode, [message]);
            }
        }
    }
}
