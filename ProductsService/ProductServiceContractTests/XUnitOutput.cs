using PactNet.Infrastructure.Outputters;
using Xunit.Abstractions;

namespace ProductServiceContractTests
{
    public class XUnitOutput : IOutput
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public XUnitOutput(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        public void WriteLine(string line)
        {
            _testOutputHelper.WriteLine(line);
        }
    }
}
