using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nois.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nois.Persistance.Configurations
{
    public class PromoCodeConfiguration : IEntityTypeConfiguration<PromoCode>
    {
		public void Configure(EntityTypeBuilder<PromoCode> builder)
		{
			builder.Property(p => p.DiscountAmount)
					.HasPrecision(18, 2);
			builder.Property(p => p.DiscountPercent)
					.HasPrecision(18, 2);
			builder.Property(p => p.MinOrderAmount)
					.HasPrecision(18, 2);
			builder.HasIndex(c => c.Code).IsUnique();
		}
	}
}
