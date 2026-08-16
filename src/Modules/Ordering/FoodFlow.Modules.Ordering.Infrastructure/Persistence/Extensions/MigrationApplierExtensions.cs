using FoodFlow.BuildingBlocks.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;

namespace FoodFlow.Modules.Ordering.Infrastructure.Persistence.Extensions;

public static class MigrationApplierExtensions
{
    public static async Task MigrateOrderingDatabaseAsync(
        this WebApplication app,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(app);
        await app.MigrateDatabaseAsync<OrderingDbContext>(cancellationToken);
    }
}
