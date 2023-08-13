using Google.Protobuf.WellKnownTypes;
using IdentityModel.Client;
using Microservice.Web.FrontEnd.Models.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using static NuGet.Packaging.PackagingConstants;
using Method = RestSharp.Method;

namespace Microservice.Web.FrontEnd.Services.OrderServices
{
    public interface IOrderService
    {
        List<OrderDto> GetOrders(string userId);
        Task<OrderDetailDto> GetOrderDetail(Guid OrderId);
        ResultDto RequestPayment(Guid OrderId);

    }
    public class OrderService : IOrderService
    {
        private readonly RestClient _restClient;
        private readonly HttpContextAccessor _httpContextAccessor;
        private  string _accesesstoken = null;
        public OrderService(RestClient restClient,HttpContextAccessor httpContextAccessor)
        {
            _restClient = restClient;
            _httpContextAccessor = httpContextAccessor;
        }
        private async Task<string> GetAccesessToken()
        {
            if (!string.IsNullOrWhiteSpace(_accesesstoken))
            {
                return _accesesstoken;
            }
            HttpClient httpClient = new HttpClient();
            var discoveryDocument = await httpClient.GetDiscoveryDocumentAsync("https://localhost:7100");
            var Token = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId= "webfrontend",
                ClientSecret="123456",
                Scope= "OrderService.FullAccess"
            }) ;
            if (Token.IsError)
            {
                throw new Exception(Token.Error);
            }
            _accesesstoken=Token.AccessToken;
            return _accesesstoken;

        }
        public async Task<OrderDetailDto> GetOrderDetail(Guid OrderId)
        {
            var request = new RestRequest($"/api/Orders/{OrderId}", method: Method.Get);

            var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = _restClient.Execute(request);
            var orderDetail=JsonConvert.DeserializeObject<OrderDetailDto>(response.Content);
            return orderDetail;
        }

        public List<OrderDto> GetOrders(string UserId)
        {
            var request = new RestRequest("/api/Orders", method: Method.Get);
          //  var token=  GetAccesessToken().Result;
            var token =  _httpContextAccessor.HttpContext.GetTokenAsync("access_token").Result;
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = _restClient.Execute(request);
            var orders = JsonConvert.DeserializeObject<List<OrderDto>>(response.Content);
            return orders;
        }

        public ResultDto RequestPayment(Guid OrderId)
        {

            var request = new RestRequest($"/api/OrderPayment?Id={OrderId}", method: Method.Post);
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
                    Message = response.ErrorMessage
                };
            }
        }
    }

    public class OrderDetailDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool OrderPaid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<OrderLineDto> OrderLines { get; set; }
        public int ItemCount { get; set; }
        public double TotalPrice { get; set; }
        public PaymentStatus paymentStatusenum { get; set; }
    }
    public class OrderLineDto
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public PaymentStatus paymentStatusenum { get; set; }

    }
    public enum PaymentStatus
    {
        /// <summary>
        /// پرداخت نشده
        /// </summary>
        unPaid = 0,
        /// <summary>
        /// درخواست پرداخت شده 
        /// </summary>
        RequestPayment = 1,
        /// <summary>
        /// پرداخت شده است
        /// </summary>
        isPaid = 3


    }
    public class OrderDto
    {
        public Guid Id { get; set; }
        public int ItemCount { get; set; }
        public double TotalPrice { get; set; }
        public bool OrderPaid { get; set; }
        public DateTime OrderPlaced { get; set; }

    }
}
