
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;
using Xunit;


namespace ProductServiceContractTests
{
    public class ProductControllerTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ProductControllerTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        [Fact]
        public void Test_Provider_Api_Pact_With_Order_Consumer()
        {
            //Arrange
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new  XUnitOutput(_testOutputHelper),
                },
                Verbose = true,
            };

            //Act

            //Assert
            new PactVerifier(config)
               .ServiceProvider("ProductServiceProvider", "https://localhost:7156")
               .HonoursPactWith("OrderServiceConsumer")
               .PactUri(@"D:\Servicess_Store\pacts\orderserviceconsumer-productserviceprovider.json")
               .Verify(description: "Prdocut information must be returened"
               , providerState: "There is Correct Data");
        

        }
    }
}
