using FoodFlow.Modules.Ordering.Domain.Aggregates.Orders;
using FoodFlow.Modules.Ordering.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodFlow.Modules.Ordering.Infrastructure.Persistence.Configurations;

internal sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public const string TableName = "OrderItems";
    
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable(TableName);
        
        builder.HasKey(x => x.Id);
        
        builder
            .Property(item => item.Id)
            .HasConversion(id => id.Value, value => new OrderItemId(value));
        
        builder
            .Property(order => order.OrderId)
            .HasConversion(orderId => orderId.Value, value => new OrderId(value))
            .IsRequired();
        
        builder
            .Property(item => item.ProductId)
            .HasConversion(productId => productId.Value, value => new ProductId(value))
            .IsRequired();
        
        builder.Property(item => item.ProductName)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(item => item.UnitPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder
            .Property(item => item.Quantity)
            .IsRequired();
    }
}