using Microsoft.EntityFrameworkCore;
using ProductsService.Models.Entites;

namespace ProductsService.Infrastructure.Contexts
{
    public class ProductsDatabaseContext:DbContext
    {
        
        public ProductsDatabaseContext(DbContextOptions<ProductsDatabaseContext> options):base(options) 
        { 
            
        }
        public virtual DbSet<Product>  Products{ get; set; }
        public virtual DbSet<Category> Categories{ get; set; }
    }
}
