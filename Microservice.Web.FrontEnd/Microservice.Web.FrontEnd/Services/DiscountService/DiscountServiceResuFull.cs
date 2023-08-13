using Microservice.Web.FrontEnd.Models.Dtos;
using Newtonsoft.Json;
using RestSharp;


namespace Microservice.Web.FrontEnd.Services.DiscountService
{
    public class DiscountServiceResuFull : IDiscountService
    {
        private readonly RestClient _restClient;

        public DiscountServiceResuFull(RestClient restClient)
        {
            _restClient = restClient;
        }
        public ResultDto<DiscountDto> GetDiscountByCode(string code)
        {
            var request=new RestRequest($"api/Discount?code={code}",Method.Get);
            var response=_restClient.Execute(request);
            var Result = JsonConvert.DeserializeObject<ResultDto<DiscountDto>>(response.Content);
            return Result;
        }

        public ResultDto<DiscountDto> GetDiscountById(Guid Id)
        {
            var request = new RestRequest($"api/Discount?Id={Id}", Method.Get);
            var response = _restClient.Execute(request);
            var Result = JsonConvert.DeserializeObject<ResultDto<DiscountDto>>(response.Content);
            return Result;
        }

        public ResultDto UseDiscount(Guid DiscountId)
        {
            var request = new RestRequest($"api/Discount?Id={DiscountId}", Method.Put);
            var response = _restClient.Execute(request);
            var Result = JsonConvert.DeserializeObject<ResultDto>(response.Content);
            return Result;
        }
    }
}
