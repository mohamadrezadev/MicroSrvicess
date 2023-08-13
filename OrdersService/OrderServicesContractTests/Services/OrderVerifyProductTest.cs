using OrdersService.Models.Services.ProductServices;
using PactNet.Matchers;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace OrderServicesContractTests.Services
{
    public class OrderVerifyProductTest:IClassFixture<ConsumerPactClassFixure>
    {
        private readonly IMockProviderService _mockProviderService;
        private readonly string _mockProviderServiceBaseUri;

        public OrderVerifyProductTest(ConsumerPactClassFixure fixure)
        {
            _mockProviderService = fixure.MockProviderService;
            _mockProviderService.ClearInteractions();
            _mockProviderServiceBaseUri = fixure.MockProviderServiceBaseUrl;
        }
        [Fact]
        public void Check_Product_Verify_Api()
        {
            //Arrange
            _mockProviderService.Given("There is Correct Data").UponReceiving("Prdocut information must be returened")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = "/api/Product/Verify/f5ec50c6-24cb-497b-8eb0-08db9909522c",
                }).WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = Match.Type(new
                    {
                        id = "f5ec50c6-24cb-497b-8eb0-08db9909522c",
                        name = "Test Name"
                    })
                });

            //Act
            IVerifyProductService verifyProduct = new VerifyProductService(new RestClient(_mockProviderServiceBaseUri));
            var result = verifyProduct.VerifyProduct(new ProductDto
            {
                ProductId = Guid.Parse("f5ec50c6-24cb-497b-8eb0-08db9909522c")
            });
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Name);
        }
    }
}
