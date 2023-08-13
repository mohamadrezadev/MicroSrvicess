using PactNet.Mocks.MockHttpService;
using PactNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderServicesContractTests
{
    public class ConsumerPactClassFixure : IDisposable
    {
        public IPactBuilder PactBuilder { get; private set; }
        public IMockProviderService MockProviderService { get; private set; }
        public int MockServerPort { get { return 1002; } }
        public string MockProviderServiceBaseUrl { get { return $"http://localhost:{MockServerPort}"; } }

        public ConsumerPactClassFixure()
        {
            var pactconfig=new PactConfig
            {
                SpecificationVersion = "2.0.0",
                PactDir= @"D:\Servicess_Store\pacts",
                LogDir=@".\pact_logs"
            };
            PactBuilder=new PactBuilder(pactconfig);
            PactBuilder.ServiceConsumer("OrderServiceConsumer").HasPactWith("ProductServiceProvider");
            MockProviderService = PactBuilder.MockService(MockServerPort);
        }

        public void Dispose()
        {
            PactBuilder.Build();
        }
    }
}
