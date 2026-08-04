using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FoodFlow.Modules.Ordering.Infrastructure.Persistence;

public sealed class OrderingDbContextFactory : IDesignTimeDbContextFactory<OrderingDbContext>
{
    public OrderingDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<OrderingDbContext>()
            .UseNpgsql("Host=localhost;Port=5432;Database=foodflow_db;Username=postgres;Password=postgrespassword",
                npgsql => npgsql.MigrationsHistoryTable("__ef_migrations_history", OrderingDbContext.DefaultSchema))
            .Options;

        return new OrderingDbContext(options);
    }
}