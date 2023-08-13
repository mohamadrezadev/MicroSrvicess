using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductsService.Controllers;
using ProductsService.Models.Services;
using ProductsServiceTest.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace ProductsServiceTest.Controllers
{
    public class ProductControllerTest
    {
        private readonly ProductMoqData productMoqData; 
        public ProductControllerTest()
        {
            productMoqData = new ProductMoqData();
        }
        [Fact]
        public void Returns_All_Products()
        {
            //Arange
            var moq = new Mock<IProductService>();
            moq.Setup(p => p.GetProducts()).Returns(productMoqData.GetProducts());
            var ProductController = new ProductController(moq.Object);
            //Act
            var result= ProductController.Get();
            //Assert
            Assert.NotNull(result);
           Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Returns_No_Products()
        {
            //Arange
            var moq = new Mock<IProductService>();
            moq.Setup(p => p.GetProducts()).Returns(productMoqData.Emptyproducts());
            var ProductController = new ProductController(moq.Object);
            //Act
            var result = ProductController.Get();
            //Assert
           // Assert.Null(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Return_Product_ByID()
        {
            //Arange
            var moq=new Mock<IProductService>();
            moq.Setup(p => p.GetProduct(Guid.NewGuid())).Returns(productMoqData.ReturnProduct());
            var ProductController=new ProductController(moq.Object);
            //Act
            var result= ProductController.Get(Guid.NewGuid());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void Returns_No_Product()
        {
            //Arange
            var moq = new Mock<IProductService>();
            moq.Setup(p => p.GetProduct(Guid.NewGuid())).Returns(productMoqData.Emptyproduct());
            var ProductController = new ProductController(moq.Object);
            //Act
            var result = ProductController.Get(Guid.NewGuid());
            //Assert
            var returnok = result as OkObjectResult;
            Assert.Null(returnok.Value);

        }
    }
}
