using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.Modules.Identity.Domain.Aggregates.Roles;
using FoodFlow.Modules.Identity.Domain.Aggregates.Users.Events;
using FoodFlow.Modules.Identity.Domain.Errors;
using FoodFlow.Modules.Identity.Domain.ValueObjects;

namespace FoodFlow.Modules.Identity.Domain.Aggregates.Users;

/// <summary>
/// Учётная запись пользователя — корень агрегата ограниченного контекста Identity.
/// Хранит идентификационные данные, хеш пароля, набор ролей и историю входов.
/// </summary>
public class User : AggregateRoot<UserId>
{
    private readonly List<Role> _roles = [];

    private User()
    {
    }

    private User(
        UserId id,
        Username username,
        Email email,
        PasswordHash passwordHash,
        PersonName name,
        PhoneNumber? phone)
        : base(id)
    {
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        Name = name;
        Phone = phone;
        IsActive = true;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Уникальное имя пользователя (логин).
    /// </summary>
    public Username Username { get; private set; } = null!;

    /// <summary>
    /// Адрес электронной почты пользователя.
    /// </summary>
    public Email Email { get; private set; } = null!;

    /// <summary>
    /// Хеш пароля пользователя (PBKDF2).
    /// </summary>
    public PasswordHash PasswordHash { get; private set; } = null!;

    /// <summary>
    /// Имя и фамилия пользователя.
    /// </summary>
    public PersonName Name { get; private set; } = null!;

    /// <summary>
    /// Номер телефона пользователя. Может быть <c>null</c>.
    /// </summary>
    public PhoneNumber? Phone { get; private set; }

    /// <summary>
    /// Признак активности учётной записи.
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// Дата и время создания учётной записи (UTC).
    /// </summary>
    public DateTimeOffset CreatedAt { get; private set; }

    /// <summary>
    /// Дата и время последнего успешного входа (UTC). <c>null</c> — если вход ещё не выполнялся.
    /// </summary>
    public DateTimeOffset? LastLoginAt { get; private set; }

    /// <summary>
    /// Дата и время последнего не успешного входа (UTC). <c>null</c> — если вход ещё не выполнялся.
    /// </summary>
    public DateTimeOffset? LastFailedLoginAt { get; private set; }

    /// <summary>
    /// Счётчик последовательных неудачных попыток входа.
    /// Сбрасывается в 0 при успешном входе.
    /// </summary>
    public int FailedLoginAttempts { get; private set; }

    /// <summary>
    /// Время, до которого учётная запись временно заблокирована из-за подбора пароля (UTC).
    /// <c>null</c> — учётная запись не заблокирована.
    /// </summary>
    public DateTimeOffset? LockedUntil { get; private set; }

    /// <summary>
    /// Признак подтверждения адреса электронной почты пользователем.
    /// </summary>
    public bool EmailVerified { get; private set; }

    /// <summary>
    /// Дата и время подтверждения email (UTC). <c>null</c> — если не подтверждался.
    /// </summary>
    public DateTimeOffset? EmailVerifiedAt { get; private set; }

    /// <summary>
    /// Возвращает <c>true</c>, если учётная запись временно заблокирована на момент <paramref name="now"/>.
    /// </summary>
    /// <param name="now">Текущее время (UTC).</param>
    public bool IsLockedOut(DateTimeOffset now) => LockedUntil is { } until && until > now;

    public IReadOnlyCollection<Role> Roles => _roles;

    public static User Register(
        Username username,
        Email email,
        PasswordHash passwordHash,
        PersonName name,
        PhoneNumber? phone)
    {
        ArgumentNullException.ThrowIfNull(username);
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(passwordHash);
        ArgumentNullException.ThrowIfNull(name);

        var user = new User(UserId.New(), username, email, passwordHash, name, phone);
        user.RaiseDomainEvent(new UserRegisteredEvent(user.Id, user.Username, user.Email));
        return user;
    }

    public void RecordLogin()
    {
        CheckIsActive();

        LastLoginAt = DateTimeOffset.UtcNow;
        FailedLoginAttempts = 0;
        LockedUntil = null;

        RaiseDomainEvent(new UserLoggedInEvent(Id, LastLoginAt.Value));
    }

    public void AssignRole(Role role)
    {
        ArgumentNullException.ThrowIfNull(role);

        CheckIsActive();

        if (_roles.Any(r => r.Id == role.Id))
        {
            return;
        }

        _roles.Add(role);
    }

    private void CheckIsActive()
    {
        if (!IsActive)
        {
            throw new DomainException(AppErrors.Domain.UserAccountIsDeactivated.New());
        }
    }
}
