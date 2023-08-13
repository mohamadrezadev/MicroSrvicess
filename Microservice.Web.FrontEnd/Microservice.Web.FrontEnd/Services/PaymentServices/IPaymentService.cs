using Microservice.Web.FrontEnd.Models.Dtos;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using RestSharp;

namespace Microservice.Web.FrontEnd.Services.PaymentServices
{
    public interface IPaymentService
    {
        ResultDto<ReturnPaymentLinkDto> GetPaymentLink(Guid OrdeId,string CallbackUrl);
    }
    public class PaymentService: IPaymentService
    {
        private readonly RestClient _restClient;

        public PaymentService(RestClient restClient)
        {
            _restClient = restClient;
        }

        public ResultDto<ReturnPaymentLinkDto> GetPaymentLink(Guid OrdeId, string CallbackUrl)
        {
            var request = new RestRequest($"/api/Payment?OrderId={OrdeId}&callbackUrlFront={CallbackUrl}", method: Method.Get);
            var response = _restClient.Execute(request);
            var orders=JsonConvert.DeserializeObject<ResultDto<ReturnPaymentLinkDto>>(response.Content);
            return orders; 
        }
    }
    public class ReturnPaymentLinkDto
    {
        public string PaymentLink { get; set; }
    }
}
