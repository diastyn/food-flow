using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders;
using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders.Enums;
using FoodFlow.Modules.Ordering.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFlow.Modules.Ordering.Infrastructure.Persistence.Configurations;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public const string TableName = "Orders";

    public void Configure(EntityTypeBuilder<Order> builder)
    {
        _ = builder.ToTable(TableName);

        _ = builder.HasKey(o => o.Id);
        _ = builder
            .Property(order => order.Id)
            .HasConversion(id => id.Value, value => new OrderId(value));

        _ = builder
            .Property(order => order.CustomerId)
            .HasConversion(id => id.Value, value => new CustomerId(value));

        _ = builder
            .Property(order => order.RestaurantId)
            .HasConversion(id => id.Value, value => new RestaurantId(value));

        _ = builder.ComplexProperty(order => order.DeliveryAddress, address =>
        {
            _ = address.Property(a => a.Country).HasMaxLength(100).HasColumnName("DeliveryAddressCountry").IsRequired();
            _ = address.Property(a => a.City).HasMaxLength(100).HasColumnName("DeliveryAddressCity").IsRequired();
            _ = address.Property(a => a.Street).HasMaxLength(200).HasColumnName("DeliveryAddressStreet").IsRequired();
            _ = address.Property(a => a.PostalCode).HasMaxLength(20).HasColumnName("DeliveryAddressPostalCode");
        });

        _ = builder
            .HasMany(order => order.Items)
            .WithOne()
            .HasForeignKey(order => order.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        _ = builder.Property(order => order.Status)
            .HasConversion(status => status.Value,
                value => OrderStatus.FromValue(value));

        _ = builder.ComplexProperty(order => order.TotalPrice, totalPrice =>
        {
            _ = totalPrice.Property(t => t.Amount)
                .HasColumnName("TotalPriceAmount")
                .HasPrecision(18, 2)
                .IsRequired();

            _ = totalPrice
                .Property(t => t.Currency)
                .HasColumnName("TotalPriceCurrency")
                .HasMaxLength(3)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code))
                .IsRequired();
        });

        _ = builder.Ignore(u => u.DomainEvents);
    }
}
