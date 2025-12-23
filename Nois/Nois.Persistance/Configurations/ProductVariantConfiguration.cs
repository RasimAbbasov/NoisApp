using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nois.Domain.Entities;
using System.Reflection.Emit;

namespace Nois.Persistance.Configurations
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {  
           builder.HasIndex(x => new { x.ProductId, x.SizeId, x.ColorId }).IsUnique();
        
        }
    }
}
