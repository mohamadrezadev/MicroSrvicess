using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductsService.Controllers;
using ProductsService.MessageingBus.SendMessage;
using ProductsService.Models.Services;
using ProductsServiceTest.MockData;
using Tynamix.ObjectFiller;

namespace ProductsServiceTest.Controllers
{
    public class ManagementProductControllerTest
    {
        private readonly ProductMoqData productMoqData;
        public ManagementProductControllerTest()
        {
            productMoqData=new ProductMoqData();
        }
        [Fact]
        public void Return_All_Products()
        {
            //Arange
            var moq = new Mock<IProductService>();
            var moqmessagebuus = new Mock<IMessagesBus>();
            moq.Setup(p => p.GetProducts()).Returns(productMoqData.GetProducts());
            var managementProduct = new ManagementProductController(moq.Object );
            //Act
            var result = managementProduct.Get();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void Return_Product_ByID()
        {
            //Arrange
            var moq = new Mock<IProductService>();
            var moqmessagebuus = new Mock<IMessagesBus>();
            moq.Setup(p => p.GetProduct(Guid.NewGuid())).Returns(productMoqData.ReturnProduct());
            var managementProduct = new ManagementProductController(moq.Object); 
            
            //Act
            var result = managementProduct.Get(Guid.NewGuid());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void Create_new_Product()
        {
            //Arrange
            var newProduct = new Filler<AddNewProductDto>().Create();
            var moqPrdouctService=new Mock<IProductService>();
            var moqmessagebuus = new Mock<IMessagesBus>();
            moqPrdouctService.Setup(p => p.AddNewProduct(newProduct));
            var managementProduct = new ManagementProductController(moqPrdouctService.Object);

            //Act
            var result=managementProduct.Post(newProduct);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedResult>(result );
        }
       
    }
}
