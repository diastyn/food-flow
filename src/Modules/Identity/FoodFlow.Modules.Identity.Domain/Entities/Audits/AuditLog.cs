using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.Modules.Identity.Domain.Entities.Audits.Enums;
using FoodFlow.Modules.Identity.Domain.ValueObjects;

namespace FoodFlow.Modules.Identity.Domain.Entities.Audits;

public class AuditLog : Entity<AuditLogId>
{
    private AuditLog()
    {
    }

    private AuditLog(
        AuditAction action,
        bool succeeded,
        string? ipAddress = null,
        string? userAgent = null,
        Guid? actorUserId = null,
        Guid? targetUserId = null,
        string? username = null,
        string? details = null)
    {
        Id = AuditLogId.New();
        OccurredAt = DateTimeOffset.UtcNow;
        Action = action;
        Succeeded = succeeded;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        ActorUserId = actorUserId;
        TargetUserId = targetUserId;
        Username = username;
        Details = details;
    }

    /// <summary>Время события (UTC).</summary>
    public DateTimeOffset OccurredAt { get; private set; }

    /// <summary>Тип действия.</summary>
    public AuditAction Action { get; private set; }

    /// <summary>Результат: успех или отказ.</summary>
    public bool Succeeded { get; private set; }

    /// <summary>Идентификатор инициатора действия. <c>null</c> — для анонимных/системных.</summary>
    public Guid? ActorUserId { get; private set; }

    /// <summary>Идентификатор затронутого пользователя.</summary>
    public Guid? TargetUserId { get; private set; }

    /// <summary>Имя пользователя/логин, фигурирующий в действии.</summary>
    public string? Username { get; private set; }

    /// <summary>IP-адрес клиента.</summary>
    public string? IpAddress { get; private set; }

    /// <summary>User-Agent клиента.</summary>
    public string? UserAgent { get; private set; }

    /// <summary>Произвольное текстовое описание события.</summary>
    public string? Details { get; private set; }

    public static AuditLog Create(
        AuditAction action,
        bool succeeded = true,
        string? ipAddress = null,
        string? userAgent = null,
        Guid? actorUserId = null,
        Guid? targetUserId = null,
        string? username = null,
        string? details = null)
    {
        var log = new AuditLog(
            action,
            succeeded,
            ipAddress,
            userAgent,
            actorUserId,
            targetUserId,
            username,
            details);

        return log;
    }
}
