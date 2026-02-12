using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nois.Domain.Entities;

namespace Nois.Persistance.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.User)
           .WithMany() // Leave empty because User doesn't have ICollection<Order>
           .HasForeignKey(o => o.BuyerId)
           .IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.TotalAmount)
                .HasPrecision(18, 2);
			builder.Property(o => o.Id)
		   .HasDefaultValueSql("NEWSEQUENTIALID()");
		}
    }
}
