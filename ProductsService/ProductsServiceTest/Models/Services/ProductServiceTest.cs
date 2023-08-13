using Moq;
using ProductsService.Infrastructure.Contexts;
using ProductsService.Models.Services;
using ProductsServiceTest.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace ProductsServiceTest.Models.Services
{
    public  class ProductServiceTest
    {
        private readonly ProductMoqData productMoqData;
        public ProductServiceTest()
        {
            productMoqData = new ProductMoqData();
        }
        [Fact]
        public void AddNewProduct()
        {
            ////Arrange
            //var newproduct = new Filler<AddNewProductDto>().Create();
            //var moq = new Mock<ProductsDatabaseContext>();
            ////moq.Setup(productMoqData=>productMoqData.Add(newproduct));
            //var ProductService=new ProductService(moq.Object);
            ////Act
            //ProductService.AddNewProduct(newproduct);
            ////Assert
            //Assert.True(true);
        }
    }
}
