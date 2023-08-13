using Microsoft.EntityFrameworkCore;
using OrdersService.Models.Entites;

namespace OrdersService.Infrastructure.Contexts
{
    public class OrderDatabaseContext:DbContext
    {
       
        public OrderDatabaseContext(DbContextOptions<OrderDatabaseContext> options) : base(options)
        {
            
        }
       
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
