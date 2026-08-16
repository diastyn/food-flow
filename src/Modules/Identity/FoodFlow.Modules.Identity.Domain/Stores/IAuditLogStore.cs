using FoodFlow.Modules.Identity.Domain.Entities.Audits;

namespace FoodFlow.Modules.Identity.Domain.Stores;

public interface IAuditLogStore
{
    public Task AddAsync(AuditLog auditLog, CancellationToken cancellationToken);
}
