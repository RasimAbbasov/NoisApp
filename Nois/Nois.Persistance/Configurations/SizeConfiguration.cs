using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nois.Domain.Entities;
using System.Reflection.Emit;

namespace Nois.Persistance.Configurations
{
    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.HasIndex(s => s.Code).IsUnique();
        }
    }
}
