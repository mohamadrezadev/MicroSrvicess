using ProductsService.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace ProductServiceIntegrationTests.Models.Services
{
    public  class ProductServiceTest:IClassFixture<ServiceFixture>
    {
        private readonly ServiceFixture _Fixture;

        public ProductServiceTest(ServiceFixture serviceFixture)
        {
            _Fixture=serviceFixture;
        }
        [Fact]
        public void Add_new_Product()
        {
            //Arrange
            var newproduct = new Filler<AddNewProductDto>().Create();
            var Catergoryid=_Fixture._categoryService.AddNewCategory(new Filler<CategoryDto>().Create());
            newproduct.CategoryId = Catergoryid;
            
            //Act
            var result=_Fixture._productService.AddNewProduct(newproduct);
            var product = _Fixture._productService.GetProduct(result);
            //Assert
            Assert.NotNull(product);
            Assert.Equal(product.id, result);
        }

        
    }
}
