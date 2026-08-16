using FoodFlow.Modules.Identity.Domain.Entities.Audits;
using FoodFlow.Modules.Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFlow.Modules.Identity.Infrastructure.Persistence.Configurations;

public class AuditLogEntityConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public const string TableName = "AuditLogs";

    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        _ = builder.ToTable(TableName);
        _ = builder.HasKey(a => a.Id);
        _ = builder.Property(a => a.Id)
            .HasConversion(a => a.Value, id => new AuditLogId(id));

        _ = builder.Property(a => a.OccurredAt).IsRequired();
        _ = builder.Property(a => a.Action).HasConversion<string>().HasMaxLength(64).IsRequired();
        _ = builder.Property(a => a.Succeeded).IsRequired();
        _ = builder.Property(a => a.Username).HasMaxLength(256);
        _ = builder.Property(a => a.IpAddress).HasMaxLength(64);
        _ = builder.Property(a => a.UserAgent).HasMaxLength(512);
        _ = builder.Property(a => a.Details).HasMaxLength(1024);

        _ = builder.HasIndex(a => a.OccurredAt);
        _ = builder.HasIndex(a => a.ActorUserId);
        _ = builder.HasIndex(a => a.TargetUserId);
        _ = builder.HasIndex(a => a.Action);
    }
}
