using FoodFlow.Modules.Identity.Domain.Entities.Audits.Enums;
using MediatR;

namespace FoodFlow.Modules.Identity.Application.Audits.Notifications.AuditEventOccurred;

/// <summary>
/// Запись для журнала аудита безопасности. IP-адрес и User-Agent добавляются реализацией
/// автоматически из контекста запроса.
/// </summary>
/// <param name="Action">Тип действия.</param>
/// <param name="ActorUserId">
/// Идентификатор инициатора действия (например, администратора). <c>null</c> — для анонимных
/// или системных действий.
/// </param>
/// <param name="TargetUserId">Идентификатор затронутого пользователя (если применимо).</param>
/// <param name="Username">
/// Имя пользователя/логин, фигурирующий в действии (полезно при неудачном входе, когда
/// <see cref="TargetUserId"/> неизвестен).
/// </param>
/// <param name="Details">Произвольное текстовое описание (например, имя роли, причина отказа).</param>
/// <param name="Succeeded">Результат действия: успех или отказ. По умолчанию <c>true</c>.</param>
public sealed record AuditEventOccurredNotification(
    AuditAction Action,
    Guid? ActorUserId = null,
    Guid? TargetUserId = null,
    string? Username = null,
    string[]? Details = null,
    bool Succeeded = true) : INotification;
