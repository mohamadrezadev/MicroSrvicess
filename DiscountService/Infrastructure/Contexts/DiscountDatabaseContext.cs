using DiscountService.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace DiscountService.Infrastructure.Contexts
{
    public class DiscountDatabaseContext:DbContext
    {
        public DiscountDatabaseContext(DbContextOptions<DiscountDatabaseContext> options):base(options)
        {

        }
        public DbSet<DiscountCode> discountCodes { get; set; }

    }
}
