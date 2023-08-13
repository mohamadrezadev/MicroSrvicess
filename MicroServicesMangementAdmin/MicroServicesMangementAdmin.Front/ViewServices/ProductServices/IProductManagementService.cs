using MicroServicesMangementAdmin.Front.Models.Dtos;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MicroServicesMangementAdmin.Front.ViewServices.ProductServices;

public interface IProductManagementService
{
    List<ProductDto> GetProductList();
    ResultDto Update(UpdateProductName updateProduct);

}
public class ProductManagementService: IProductManagementService
{
    private readonly RestClient _restClient;

    public ProductManagementService(RestClient restClient)
    {
        _restClient = restClient;
    }


    public List<ProductDto> GetProductList()
    {
        var request = new RestRequest(method: Method.Get, resource: "/api/ManagementProduct");
        var response = _restClient.Execute(request);
        var Products = JsonConvert.DeserializeObject<List<ProductDto>>(response.Content);
        return Products;
    }

    public ResultDto Update(UpdateProductName updateProduct)
    {
        var request = new RestRequest($"/api/ManagementProduct", Method.Put);
        request.AddHeader("Content-Type", "application/json");
        string serializeModel = JsonSerializer.Serialize(updateProduct);
        request.AddParameter("application/json", serializeModel, ParameterType.RequestBody);
        var response = _restClient.Execute(request);
        return GetResponseStatusCode(response);
       
    }
    private static ResultDto GetResponseStatusCode(RestResponse restResponse)
    {
        if (restResponse.StatusCode==HttpStatusCode.OK)
        {
            return new ResultDto(true);
        }
        return new ResultDto(false,restResponse.ErrorMessage);
    }
}
public record UpdateProductName(Guid ProductId, string Name,double Price);
public record ProductDto(Guid id,string? name,string? description,string? img,double Price, ProductCategoryDto? Category);
public record ProductCategoryDto(Guid categoryid,string category);