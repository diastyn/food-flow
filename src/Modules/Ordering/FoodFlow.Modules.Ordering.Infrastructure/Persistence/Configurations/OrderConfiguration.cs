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
        builder.ToTable(TableName);

        builder.HasKey(o => o.Id);
        builder
            .Property(order => order.Id)
            .HasConversion(id => id.Value, value => new OrderId(value));

        builder
            .Property(order => order.CustomerId)
            .HasConversion(id => id.Value, value => new CustomerId(value));

        builder
            .Property(order => order.RestaurantId)
            .HasConversion(id => id.Value, value => new RestaurantId(value));

        builder.ComplexProperty(order => order.DeliveryAddress, address =>
        {
            address.Property(a => a.Country).HasMaxLength(100).HasColumnName("DeliveryAddressCountry").IsRequired();
            address.Property(a => a.City).HasMaxLength(100).HasColumnName("DeliveryAddressCity").IsRequired();
            address.Property(a => a.Street).HasMaxLength(200).HasColumnName("DeliveryAddressStreet").IsRequired();
            address.Property(a => a.PostalCode).HasMaxLength(20).HasColumnName("DeliveryAddressPostalCode");
        });

        builder
            .HasMany(order => order.Items)
            .WithOne()
            .HasForeignKey(order => order.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(order => order.Status)
            .HasConversion(status => status.Value, 
                value => OrderStatus.FromValue(value));

        builder.ComplexProperty(order => order.TotalPrice, totalPrice =>
        {
            totalPrice.Property(t => t.Amount)
                .HasColumnName("TotalPriceAmount")
                .HasPrecision(18, 2)
                .IsRequired();
            
            totalPrice
                .Property(t => t.Currency)
                .HasColumnName("TotalPriceCurrency")
                .HasMaxLength(3)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code))
                .IsRequired();
        });
    }
}