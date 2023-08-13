
using Microservice.Web.FrontEnd.Models.Dtos;
using NuGet.ContentModel;
using RestSharp;
using System.Text.Json;
namespace Microservice.Web.FrontEnd.Services.BasketService
{
    public class BasketSerivce : IBasketSerivce
    {
        private readonly RestClient _restClient;

        public BasketSerivce(RestClient restClient)
        {
            _restClient = restClient;
        }

        public ResultDto AddToBasket(AddToBasketDto addToBasket, string UserId)
        {
            var request = new RestRequest($"/api/Basket?Userid={UserId}", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var Serializedto = JsonSerializer.Serialize(addToBasket);
            request.AddParameter("application/json", Serializedto, ParameterType.RequestBody);
            var response = _restClient.Execute(request);
            return GetResponseStatusCode(response);

        }

        private static ResultDto GetResponseStatusCode(RestResponse response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new ResultDto
                {
                    IsSuccess = true,

                };
            }
            else
            {
                return new ResultDto
                {
                    IsSuccess = false,

                };
            }
        }

        public BasketDto GetBasket(string UserId)
        {
            var request = new RestRequest($"/api/Basket?userid={UserId}", Method.Get);
            var response = _restClient.Execute(request);
            var basket = JsonSerializer.Deserialize<BasketDto>(response.Content);
            return basket;
        }

        public ResultDto DeleteFromBasket(Guid Iditem)
        {
            var request = new RestRequest($"/api/Basket/{Iditem}", Method.Delete);
            var response = _restClient.Execute(request);
            return GetResponseStatusCode(response);
        }

        public ResultDto UpdateQuantity(Guid basketitemid, int quantity)
        {
            var request = new RestRequest($"/api/Basket?basketitemid={basketitemid}&quantity={quantity}", Method.Put);
            var response = _restClient.Execute(request);
            return GetResponseStatusCode(response);
        }

        public ResultDto ApplyDiscountToBasket(Guid basketitemid, Guid DiscountId)
        {
           
            var request = new RestRequest($"/api/Basket/{basketitemid}/{DiscountId}", Method.Put);
            var response = _restClient.Execute(request);
            return GetResponseStatusCode(response);
        }

       
        
       public ResultDto Checkout(CheckoutDto checkout)
        {
            var request = new RestRequest($"/api/Basket/CheckoutBaskt", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var Serializedto = JsonSerializer.Serialize(checkout);
            request.AddParameter("application/json", Serializedto, ParameterType.RequestBody);
            var response = _restClient.Execute(request);
            return GetResponseStatusCode(response);
        }
    }



}
