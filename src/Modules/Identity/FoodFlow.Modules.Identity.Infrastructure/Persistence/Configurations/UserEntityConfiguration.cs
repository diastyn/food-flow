using FoodFlow.Modules.Identity.Domain.Aggregates.Users;
using FoodFlow.Modules.Identity.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFlow.Modules.Identity.Infrastructure.Persistence.Configurations;

internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public const string TableName = "Users";

    public void Configure(EntityTypeBuilder<User> builder)
    {
        _ = builder.ToTable(TableName);

        _ = builder.HasKey(x => x.Id);

        _ = builder
            .Property(x => x.Id)
            .HasConversion(u => u.Value, id => new UserId(id));

        _ = builder
            .Property(x => x.Username)
            .HasConversion(x => x.Value, name => Username.Create(name))
            .HasMaxLength(Username.MaxLength)
            .IsRequired();

        _ = builder
            .Property(x => x.Email)
            .HasConversion(x => x.Value, name => Email.Create(name))
            .HasMaxLength(Email.MaxLength)
            .IsRequired();

        _ = builder.HasIndex(x => x.Username).IsUnique();
        _ = builder.HasIndex(x => x.Email).IsUnique();

        _ = builder
            .Property(x => x.PasswordHash)
            .HasConversion(x => x.Value, name => PasswordHash.FromHash(name))
            .IsRequired();

        _ = builder.ComplexProperty(x => x.Name, name =>
            {
                _ = name.Property(n => n.FirstName).HasColumnName("FirstName").HasMaxLength(100).IsRequired();
                _ = name.Property(n => n.LastName).HasColumnName("LastName").HasMaxLength(100).IsRequired();
                _ = name.Property(n => n.FullName).HasColumnName("FullName").HasMaxLength(256);
            });

        _ = builder.Property(x => x.Phone)
            .HasConversion(p => p == null ? null : p.Value, name => string.IsNullOrEmpty(name) ? null : PhoneNumber.Create(name))
            .IsRequired(false);

        _ = builder.Property(u => u.IsActive);
        _ = builder.Property(u => u.CreatedAt);
        _ = builder.Property(u => u.LastLoginAt);
        _ = builder.Property(u => u.LastFailedLoginAt);
        _ = builder.Property(u => u.FailedLoginAttempts);
        _ = builder.Property(u => u.LockedUntil);
        _ = builder.Property(u => u.EmailVerified);
        _ = builder.Property(u => u.EmailVerifiedAt);

        _ = builder.Ignore(u => u.DomainEvents);

        _ = builder.HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity(join => join.ToTable("UserRoles"));
        _ = builder.Navigation(u => u.Roles).UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
