using Newtonsoft.Json;
using RestSharp;

namespace OrdersService.Models.Services.ProductServices
{
    public interface IVerifyProductService
    {
        VerifyProductDto VerifyProduct(ProductDto product);
    }
    public class VerifyProductService : IVerifyProductService
    {
        private readonly RestClient _restClient;

        public VerifyProductService(RestClient restClient)
        {
            _restClient = restClient;
        }
        public VerifyProductDto VerifyProduct(ProductDto product)
        {
            var request = new RestRequest($"/api/Product/Verify/{product.ProductId}");
            var response=_restClient.Execute(request);
            var productonremote=JsonConvert.DeserializeObject<ProductVerifyOnServerProductDto>(response.Content);
            return Verify(product,productonremote);
            
        }
        private VerifyProductDto Verify(ProductDto local,ProductVerifyOnServerProductDto remote)
        {
            if (local.ProductName==remote.Name)
            {
                return new VerifyProductDto(local.ProductName, IsCorrect: true);
            }
            return new VerifyProductDto(remote.Name, false);
        }
    }
    public record VerifyProductDto(string Name,bool IsCorrect);

    public record ProductVerifyOnServerProductDto(Guid Id,string Name);

  
}
