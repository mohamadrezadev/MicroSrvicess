using Microsoft.EntityFrameworkCore;
using ProductsService.Infrastructure.Contexts;
using ProductsService.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductServiceIntegrationTests
{
    public class ServiceFixture:IDisposable
    {
        public IProductService _productService { get; }
        public ICategoryService _categoryService { get; }
        public ProductsDatabaseContext _databaseContext { get; }


        public ServiceFixture()
        {
            Random random=new Random();
            DbContextOptionsBuilder<ProductsDatabaseContext> builder =
                new DbContextOptionsBuilder<ProductsDatabaseContext>();
            builder.UseSqlite($"Data Source=ProductsDBIntegrationTests{random.Next(99999)}.db");

            _databaseContext = new ProductsDatabaseContext(builder.Options);
            _databaseContext.Database.EnsureCreated();
            _productService = new ProductService(_databaseContext);
            _categoryService = new CategoryService(_databaseContext);

        }

        public void Dispose()
        {
         _databaseContext.Database.EnsureDeleted();
        }
    }
}
