using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nois.Domain.Entities;
using System.Reflection.Emit;

namespace Nois.Persistance.Configurations
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.HasIndex(w => new { w.UserId, w.ProductId })
                    .IsUnique();

        }
    }
}
