using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nois.Domain.Entities;

namespace Nois.Persistance.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(p => p.PriceAtPurchase)
                .HasPrecision(18, 2);
			builder.HasOne(oi => oi.Order)
			 .WithMany(o => o.OrderItems)
			  .HasForeignKey(oi => oi.OrderId);
			builder.HasOne(oi => oi.Order)
			.WithMany(o => o.OrderItems)
			.HasForeignKey(oi => oi.OrderId)
			.OnDelete(DeleteBehavior.Cascade);
		}
    }
}
