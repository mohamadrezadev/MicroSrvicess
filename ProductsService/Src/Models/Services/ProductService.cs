using Microsoft.EntityFrameworkCore;
using ProductsService.Infrastructure.Contexts;
using ProductsService.Models.Entites;

namespace ProductsService.Models.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductsDatabaseContext _context;

        public ProductService(ProductsDatabaseContext context)
        {
            _context = context;
        }

        public Guid AddNewProduct(AddNewProductDto product)
        {
            var Category=_context.Categories.Include(p=>p.Products).FirstOrDefault( p=>p.id==product.CategoryId);
            if (Category == null)
                
                throw new Exception("Catrgory Not Found ....");
            Product newProduct = new Product()
            {
                Category=Category,
                Name = product.Name,
                Description = product.Description,
                Image=product.Image,
                Price = product.Price,
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return newProduct.id;
        }

        public ProductDto GetProduct(Guid id)
        {
            var product = _context.Products.Include(p => p.Category)
                .SingleOrDefault(p => p.id == id);
            if (product == null)
                throw new Exception("Product Note Founded ....");
            return new ProductDto
            {
                Description = product.Description,
                Image = product.Image,
                Name = product.Name,
                Price = product.Price,
                id = product.id,
                productCategory = new ProductCategoryDto
                {
                    Category = product.Category.Name,
                    CategoryId = product.CategoryId,
                },

            };


        }

        public List<ProductDto> GetProducts()
        {
            var data=_context.Products
                .Include(p=>p.Category)
                .OrderByDescending(p=>p.id)
                .Select(p=>new ProductDto
                {
                    id = p.id,
                    Description=p.Description,
                    Name = p.Name,
                    Image = p.Image,
                    Price = p.Price,
                    productCategory=new ProductCategoryDto
                    {
                        Category=p.Category.Name,
                        CategoryId=p.CategoryId
                    }
                }).ToList();
            return data;
        }

        public bool UpdateProduct(UpdateProductDto updateProduct)
        {
            var product = _context.Products.Find(updateProduct.ProductId);
            if (product is not null)
            {
                product.Name = updateProduct.Name;
                product.Price= updateProduct.Price;
                _context.SaveChanges();
                return true;
            }
            else
                return false;
            
        }
    }
}
