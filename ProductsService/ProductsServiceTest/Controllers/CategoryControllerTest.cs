using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductsService.Controllers;
using ProductsService.Models.Services;
using ProductsServiceTest.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace ProductsServiceTest.Controllers
{
    public class CategoryControllerTest
    {
        private readonly CategoryMockData _CategoryMockData;

        public CategoryControllerTest()
        {
            _CategoryMockData = new CategoryMockData();
        }
        [Fact]
        public void Return_All_Category()
        {
            //Arenge
            var moq=new Mock<ICategoryService>();
            moq.Setup(p => p.GetCategories()).Returns(_CategoryMockData.GetCategories());
            CategoryController categoryController = new CategoryController(moq.Object);
            //Act
            var result = categoryController.Get();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var returnok=result as OkObjectResult;
            Assert.NotNull(returnok);
            Assert.IsType<List<CategoryDto>>(returnok.Value);
        }

        [Fact]
        public void Return_Empty_Category()
        {
            //Arenge
            var moq = new Mock<ICategoryService>();
            moq.Setup(p => p.GetCategories()).Returns(_CategoryMockData.Emptycategory());
            CategoryController categoryController = new CategoryController(moq.Object);
            //Act
            var result = categoryController.Get();

            //Assert
           
            Assert.IsType<OkObjectResult>(result);
        }
        
        public void Create_Category()
        {
            //Arrnge
            var newCaregory = new Filler<CategoryDto>().Create();
            var moq=new Mock<ICategoryService>();
            moq.Setup(p => p.AddNewCategory(newCaregory));
            CategoryController categoryController = new CategoryController(moq.Object);
            //Act
            var result = categoryController.Post(newCaregory);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<CreatedResult>(result);

        }
    }
}
