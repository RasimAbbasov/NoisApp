using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nois.Domain.Entities;

namespace Nois.Persistance.Configurations
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasIndex(b => b.BuyerId).IsUnique();
            builder.Property(b => b.RowVersion).IsRowVersion();
        }
    }
}
