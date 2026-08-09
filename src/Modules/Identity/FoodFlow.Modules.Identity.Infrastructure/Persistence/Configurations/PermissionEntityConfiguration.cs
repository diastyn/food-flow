using FoodFlow.Modules.Identity.Domain.Entities;
using FoodFlow.Modules.Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFlow.Modules.Identity.Infrastructure.Persistence.Configurations;

internal sealed class PermissionEntityConfiguration : IEntityTypeConfiguration<Permission>
{
    public const string TableName = "Permissions";

    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        _ = builder.ToTable(TableName);

        _ = builder.HasKey(p => p.Id);

        _ = builder.Property(p => p.Id)
            .HasConversion(p => p.Value, id => new PermissionId(id));

        _ = builder.Property(p => p.Name).HasMaxLength(128).IsRequired();
        _ = builder.HasIndex(p => p.Name).IsUnique();
        _ = builder.Property(p => p.Description).HasMaxLength(256);
    }
}
