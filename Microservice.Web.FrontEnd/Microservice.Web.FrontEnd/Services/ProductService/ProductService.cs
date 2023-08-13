using RestSharp;
using System.Text.Json;

namespace Microservice.Web.FrontEnd.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly RestClient restClient;

        public ProductService(RestClient restClient)
        {
            this.restClient = restClient;
        }
        public ProductDto GetProductById(Guid id)
        {
            var request = new RestRequest($"api/Product/{id}", method: Method.Get);
            var response = restClient.Execute(request);
            var product = JsonSerializer.Deserialize<ProductDto>(response.Content);
            return product;
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            

            var request = new RestRequest(method: Method.Get, resource: "/api/Product");
            RestResponse response = restClient.Execute(request);
            Console.WriteLine(response.Content);
            var Products = JsonSerializer.Deserialize<List<ProductDto>>(response.Content);
            return Products;


        }
    }

}
