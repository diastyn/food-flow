using FoodFlow.Modules.Identity.Domain.Aggregates.Roles;
using FoodFlow.Modules.Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFlow.Modules.Identity.Infrastructure.Persistence.Configurations;

internal sealed class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
    public const string TableName = "Roles";

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        _ = builder.ToTable(TableName);

        _ = builder.HasKey(r => r.Id);

        _ = builder.Property(r => r.Id)
            .HasConversion(r => r.Value, id => new RoleId(id));

        _ = builder.Property(r => r.Name).HasMaxLength(64).IsRequired();
        _ = builder.HasIndex(r => r.Name).IsUnique();
        _ = builder.Property(r => r.Description).HasMaxLength(256);

        _ = builder.Ignore(r => r.DomainEvents);

        _ = builder.HasMany(r => r.Permissions)
            .WithMany()
            .UsingEntity(join => join.ToTable("RolePermissions"));
        _ = builder.Navigation(r => r.Permissions).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
