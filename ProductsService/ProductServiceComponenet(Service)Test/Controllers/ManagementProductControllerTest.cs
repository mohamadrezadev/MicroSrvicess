using Microsoft.AspNetCore.Mvc;
using ProductsService.Controllers;
using ProductsService.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xunit;

namespace ProductServiceComponenet_Service_Test.Controllers;
public class ManagementProductControllerServiceTest : IClassFixture<ServiceFixture>
{
    private readonly ServiceFixture fixture;
    public ManagementProductControllerServiceTest(ServiceFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public void Add_new_Product_In_DataBase()
    {
        //Arrange
        var newproduct = new Filler<AddNewProductDto>().Create();
        var newcategory = new Filler<CategoryDto>().Create();
        Guid categoryid = fixture._categoryService.AddNewCategory(newcategory);
        newproduct.CategoryId = categoryid;

        ManagementProductController managementProductController =
            new ManagementProductController(fixture._productService);
        //Act
        var result = managementProductController.Post(newproduct) as CreatedResult;

        //Assert
        var product=fixture._productService.GetProduct(Guid.Parse(result.Value.ToString()));
        Assert.NotNull(result);
        Assert.Equal(newproduct.Price, product.Price);
        Assert.Equal(newproduct.Name, product.Name);
    }
}




