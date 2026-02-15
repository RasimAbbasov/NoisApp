using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nois.Domain.Entities;
using Nois.Domain.Entities.Identity;

namespace Nois.Persistance.Contexts
{
    public class NoisDbContext : IdentityDbContext<AppUser>
    {
        public NoisDbContext(DbContextOptions<NoisDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ProductVariantRating> ProductVariantRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NoisDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

          

         

     

            

           

        }
    }
}
