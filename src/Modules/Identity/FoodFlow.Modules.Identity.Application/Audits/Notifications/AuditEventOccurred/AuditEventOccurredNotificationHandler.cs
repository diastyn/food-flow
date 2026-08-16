using FoodFlow.BuildingBlocks.Domain.Primitives;
using FoodFlow.BuildingBlocks.Domain.Security;
using FoodFlow.Modules.Identity.Domain.Entities.Audits;
using FoodFlow.Modules.Identity.Domain.Stores;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FoodFlow.Modules.Identity.Application.Audits.Notifications.AuditEventOccurred;

public sealed class AuditEventOccurredNotificationHandler(
    ILogger<AuditEventOccurredNotificationHandler> logger,
    IRequestContext requestContext,
    IAuditLogStore logStore,
    [FromKeyedServices(nameof(Identity))]
    IUnitOfWork unitOfWork)
    : INotificationHandler<AuditEventOccurredNotification>
{
    public async Task Handle(
        AuditEventOccurredNotification notification,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(notification);

        try
        {
            var derails = string.Join(
                Environment.NewLine,
                notification.Details ?? []);
            var auditLog = AuditLog.Create(
                notification.Action,
                succeeded: notification.Succeeded,
                ipAddress: requestContext.IpAddress,
                userAgent: requestContext.UserAgent,
                actorUserId: notification.ActorUserId ?? requestContext.UserId,
                targetUserId: notification.TargetUserId,
                username: notification.Username,
                details: derails);

            await logStore.AddAsync(auditLog, cancellationToken);

            // Аудит должен сохраняться независимо от того, дойдёт ли вызывающий
            // хендлер до своего собственного SaveChangesAsync (например, при раннем
            // return на ветке ошибки).
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
#pragma warning disable CA1031 // Аудит не должен срывать основную операцию
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to write audit log entry for action {Action}", notification.Action);
        }
#pragma warning restore CA1031
    }
}
