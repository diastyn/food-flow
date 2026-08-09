using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders;
using Microsoft.EntityFrameworkCore;

namespace FoodFlow.Modules.Ordering.Infrastructure.Persistence;

internal sealed class OrderingDbContext(DbContextOptions<OrderingDbContext> options) : DbContext(options)
{
    public const string DefaultSchema = "ordering";

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        _ = modelBuilder.HasDefaultSchema(DefaultSchema);
        _ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderingDbContext).Assembly);
    }
}
