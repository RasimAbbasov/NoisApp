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
    public class ProductVariantRatingConfiguration : IEntityTypeConfiguration<ProductVariantRating>
    {
		public void Configure(EntityTypeBuilder<ProductVariantRating> builder)
		{
			builder.HasIndex(x => new { x.ProductVariantId, x.UserId }).IsUnique();
		}
	}
}
