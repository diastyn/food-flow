namespace FoodFlow.Modules.Identity.Domain.Entities.Audits.Enums;

/// <summary>
/// Тип действия, фиксируемого в журнале аудита безопасности.
/// </summary>
public enum AuditAction
{
    RegistrationFailed,

    /// <summary>Успешный вход в систему.</summary>
    LoginSucceeded,

    /// <summary>Неудачная попытка входа (неверный пароль или несуществующий пользователь).</summary>
    LoginFailed,

    /// <summary>Вход заблокирован: учётная запись временно заблокирована из-за подбора пароля.</summary>
    LoginBlockedLockedOut,

    /// <summary>Вход заблокирован: учётная запись деактивирована.</summary>
    LoginBlockedDeactivated,

    /// <summary>Вход заблокирован: email не подтверждён.</summary>
    LoginBlockedEmailNotVerified,

    /// <summary>Учётная запись автоматически заблокирована из-за превышения числа неудачных попыток.</summary>
    AccountLockedOut,

    /// <summary>Пароль изменён пользователем.</summary>
    PasswordChanged,

    /// <summary>Пароль сброшен по ссылке восстановления.</summary>
    PasswordReset,

    /// <summary>Email подтверждён.</summary>
    EmailVerified,

    /// <summary>Пользователю назначена роль.</summary>
    RoleAssigned,

    /// <summary>С пользователя снята роль.</summary>
    RoleRemoved,

    /// <summary>Обнаружено переиспользование уже использованного refresh-токена (возможная компрометация).</summary>
    RefreshTokenReuseDetected,
}
