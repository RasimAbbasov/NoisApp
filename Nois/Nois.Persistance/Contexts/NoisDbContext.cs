using Microsoft.EntityFrameworkCore;
using Nois.Domain.Entities;

namespace Nois.Persistance.Contexts
{
    public class NoisDbContext : DbContext
    {
        public NoisDbContext(DbContextOptions<NoisDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductStock> Stocks { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NoisDbContext).Assembly);

            modelBuilder.Entity<ProductStock>()
            .HasIndex(x => new { x.ProductId, x.SizeId, x.ColorId })
            .IsUnique();

            modelBuilder.Entity<Size>()
                .HasIndex(s => s.Code)
                .IsUnique();

            modelBuilder.Entity<Color>()
                .HasIndex(c => c.Code)
                .IsUnique();
        }
    }
}
