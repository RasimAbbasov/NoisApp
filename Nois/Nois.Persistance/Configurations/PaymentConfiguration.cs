using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nois.Domain.Entities;

namespace Nois.Persistance.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(p => p.Amount)
                .HasPrecision(18, 2);
			builder.Property(p => p.TransactionId)
		   .IsRequired()
		   .HasMaxLength(200);

			builder.HasOne(p => p.Order)
				.WithOne(o => o.Payment)
				.HasForeignKey<Payment>(p => p.OrderId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasIndex(p => p.OrderId)
				.IsUnique();
			builder.Property(p => p.PaidAt)
			.HasDefaultValueSql("GETUTCDATE()");
		}
    }
}
