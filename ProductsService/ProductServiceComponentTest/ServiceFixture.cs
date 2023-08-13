using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using ProductsService.Infrastructure.Contexts;
using ProductsService.MessageingBus.Config;
using ProductsService.MessageingBus.SendMessage;
using ProductsService.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace ProductServiceComponentTest
{
    public class ServiceFixture
    {
        public IProductService _productService { get; }
        public ICategoryService  _categoryService { get; }
        public IMessageBus  _messageBus { get; }
        public ProductsDatabaseContext _databaseContext { get; }

        
        public ServiceFixture()
        {
     
            DbContextOptionsBuilder<ProductsDatabaseContext> builder=
                new DbContextOptionsBuilder<ProductsDatabaseContext>();
            builder.UseInMemoryDatabase("ProductDatabasetest");
            _databaseContext = new ProductsDatabaseContext(builder.Options);
            _productService = new ProductsService.Models.Services.ProductService(_databaseContext);
            _categoryService = new CategoryService(_databaseContext);
       
        }
    }
}
