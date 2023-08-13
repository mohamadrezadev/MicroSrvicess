using ProductsService.Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace ProductsServiceTest.Models.Entites
{
    public class ProductTest
    {
        [Fact]
        public void Update_Price_Product()
        {

            //Arange
            Product newproduct = new Product()
            {
                CategoryId = Guid.NewGuid(),
                Name = "xp product",
                Description = "this is a test Description",
                id = Guid.NewGuid(),
                Image = "1.png",
                Price = 1000

            };
            double updatePrice = 125587.2;
            //Act
            newproduct.UpdatePrice(updatePrice);

            //Assert
            Assert.Equal(updatePrice, newproduct.Price);
            
        }
        [Fact]
        public void Update_Price_Product_with_zero_Value_Exception()
        {
            //Arenge
            var product =new Filler<Product>().Create();
            double updatePrice = 0;

            //Act
            
            //Assert
            Assert.Throws<Exception>(() => product.UpdatePrice(0));
        }
    }
}
