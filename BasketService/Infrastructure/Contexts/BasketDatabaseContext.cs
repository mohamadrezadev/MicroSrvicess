using BasketService.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace BasketService.Infrastructure.Contexts
{
    public class BasketDatabaseContext:DbContext
    {
        public BasketDatabaseContext(DbContextOptions<BasketDatabaseContext> options):base(options)
        {

        
        }
        protected override void OnModelCreating(ModelBuilder Builder)
        {
            Builder.Entity<Basket>().HasMany(b => b.Items).WithOne(b => b.Basket);
            Builder.Entity<BasketItem>().HasOne(b => b.Product).WithMany(p => p.basketItems);    

            base.OnModelCreating(Builder);
        }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
