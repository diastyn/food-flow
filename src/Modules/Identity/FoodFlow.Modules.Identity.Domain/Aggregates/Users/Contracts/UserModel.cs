namespace FoodFlow.Modules.Identity.Domain.Aggregates.Users.Contracts;

public sealed class UserModel
{
    public Guid Id { get; init; }

    /// <summary>
    /// Уникальное имя пользователя (логин).
    /// </summary>
    public string Username { get; init; } = null!;

    /// <summary>
    /// Адрес электронной почты пользователя.
    /// </summary>
    public string Email { get; init; } = null!;

    /// <summary>
    /// Имя и фамилия пользователя.
    /// </summary>
    public PersonNameModel Name { get; init; } = null!;

    /// <summary>
    /// Номер телефона пользователя. Может быть <c>null</c>.
    /// </summary>
    public string? Phone { get; init; }

    /// <summary>
    /// Признак активности учётной записи.
    /// </summary>
    public bool IsActive { get; init; }

    /// <summary>
    /// Дата и время создания учётной записи (UTC).
    /// </summary>
    public DateTimeOffset CreatedAt { get; init; }

    /// <summary>
    /// Дата и время последнего успешного входа (UTC). <c>null</c> — если вход ещё не выполнялся.
    /// </summary>
    public DateTimeOffset? LastLoginAt { get; init; }

    /// <summary>
    /// Дата и время последнего не успешного входа (UTC). <c>null</c> — если вход ещё не выполнялся.
    /// </summary>
    public DateTimeOffset? LastFailedLoginAt { get; init; }

    /// <summary>
    /// Счётчик последовательных неудачных попыток входа.
    /// Сбрасывается в 0 при успешном входе.
    /// </summary>
    public int FailedLoginAttempts { get; init; }

    /// <summary>
    /// Время, до которого учётная запись временно заблокирована из-за подбора пароля (UTC).
    /// <c>null</c> — учётная запись не заблокирована.
    /// </summary>
    public DateTimeOffset? LockedUntil { get; init; }

    /// <summary>
    /// Признак подтверждения адреса электронной почты пользователем.
    /// </summary>
    public bool EmailVerified { get; init; }

    /// <summary>
    /// Дата и время подтверждения email (UTC). <c>null</c> — если не подтверждался.
    /// </summary>
    public DateTimeOffset? EmailVerifiedAt { get; init; }
}
