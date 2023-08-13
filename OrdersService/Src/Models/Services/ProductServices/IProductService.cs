using OrdersService.Infrastructure.Contexts;
using OrdersService.Models.Entites;
using System.ComponentModel.DataAnnotations;

namespace OrdersService.Models.Services.ProductServices
{
    public interface IProductService
    {
        Product GetProduct(ProductDto product);
        bool UpdateProduct(Guid ProductId, string Name);
    }
    public class ProductService : IProductService
    {
        private readonly OrderDatabaseContext _DbContect;

        public ProductService(OrderDatabaseContext orderDatabaseContext)
        {
            _DbContect = orderDatabaseContext;
        }
        public Product GetProduct(ProductDto product)
        {
            var existProduct=_DbContect.Products.SingleOrDefault(p=>p.ProductId == product.ProductId);
            if (existProduct==null)
            {
               var res=  CreateNewProduct(product);
            }
            return existProduct;
        }

        public bool UpdateProduct(Guid ProductId, string Name)
        {
            var product = _DbContect.Products.Find(ProductId);
            if (product is not null)
            {
                product.ProductName = Name;
                _DbContect.SaveChanges();
            }
            return true;
        }

        private Product CreateNewProduct(ProductDto productdto)
        {
            Product product =new Product() {
                ProductId=productdto.ProductId,
                ProductName=productdto.ProductName,
                Price=productdto.Price,

            } ;
            _DbContect.Products.Add(product);
            _DbContect.SaveChanges();
            return product;
            
        }
    }
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}
