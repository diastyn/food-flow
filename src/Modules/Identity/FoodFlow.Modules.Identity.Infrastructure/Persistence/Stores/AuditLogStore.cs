using FoodFlow.Modules.Identity.Domain.Entities.Audits;
using FoodFlow.Modules.Identity.Domain.Stores;

namespace FoodFlow.Modules.Identity.Infrastructure.Persistence.Stores;

internal sealed class AuditLogStore(
    IdentityDbContext dbContext) : IAuditLogStore
{
    public async Task AddAsync(
        AuditLog auditLog,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(auditLog);
        _ = await dbContext.AuditLogs.AddAsync(auditLog, cancellationToken);
    }
}
