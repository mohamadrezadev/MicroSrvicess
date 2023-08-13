using BasketService.Infrastructure.Contexts;

namespace BasketService.Models.Services.ProductServices
{
    public interface IProductService
    {
        bool UpdateProduct(Guid ProductId, string productName,double Price);
    }
    public class ProductService : IProductService
    {
        private readonly BasketDatabaseContext _context;

        public ProductService(BasketDatabaseContext context)
        {
            _context = context;
        }
        public bool UpdateProduct(Guid ProductId, string productName, double Price)
        {
            var product = _context.Products.Find(ProductId);
            if (product is not null)
            {
                product.ProductName = productName;
                product.UnitPrice = Price;
                _context.SaveChanges();
            }
            return true;
        }
    }
}
