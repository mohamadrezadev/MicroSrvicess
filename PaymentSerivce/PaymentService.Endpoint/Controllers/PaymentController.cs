using Dto.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using PaymentService.Application.Service.PaymentServices;
using PaymentService.Domain.Orders;
using PaymentService.Endpoint.Models;
using PaymentService.Infrastructure.MessagingBus;
using PaymentService.Infrastructure.MessagingBus.Messages;
using PaymentService.Infrastructure.MessagingBus.SendPaymentMessage;
using RabbitMQ.Client;
using RestSharp;
using ZarinPal.Class;

namespace PaymentService.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ZarinPal.Class.Payment _Pyament;
        private readonly Authority _authority;
        private readonly Transactions   _transactions;
        private readonly IPaymentService _paymentService;
        private readonly string merchendId;
        private readonly IMessageBus _messageBus;
        private readonly string _queueName;
        public PaymentController(IPaymentService paymentService,
            IConfiguration configuration,IMessageBus messageBus, IOptions<RabbitMqConfiguration> options)
        {
            var expose = new Expose();
            _Pyament=expose.CreatePayment();
            _authority=expose.CreateAuthority();
            _transactions=expose.CreateTransactions();
            _paymentService = paymentService;
            merchendId = configuration["merchendId"];
            _messageBus = messageBus;
            _queueName = options.Value.QueueName_PaymentDone;

        }
        [HttpGet]
        public async Task<ActionResult<ResultDto<ReturnPaymentLinkDto>>> Get(Guid OrderId,string callbackUrlFront)
        {
            //AD72521E - 5DFB - 42B6 - 847E-54466AEEA123
            //{ d49b68fe - ed40 - 4f47 - 828c - 16017d33a85e}
            var pay=_paymentService.GetPaymentofOrder(OrderId);
            if (pay == null)
            {
                var result = new ResultDto()
                {
                    IsSuccess = false,
                    Message = "سفارش یافت نشد"
                };
                return Ok(result);
            }
            var callbackUrl = Url.ActionLink( nameof(Verify), "Payment",  new {  pay.PaymentId,callbackUrlFront},
                protocol: Request.Scheme);
            var Result = await _Pyament.Request(new DtoRequest()
            {
                Amount = (int)pay.Amount,
                CallbackUrl= callbackUrl,
                Description="TEST",
                Email="",
                Mobile="",
                MerchantId=merchendId,
            },Payment.Mode.sandbox); ;
            if (Result.Status!=100)
            {
                return BadRequest(Result);
            }
            string readirectrl = $"https://sandbox.zarinpal.com/pg/StartPay/{Result.Authority}";
            //string readirectrl = $"https://sandbox.zarinpal.com/pg/StartPay/{Result.Authority}";
            return Ok(new ResultDto<ReturnPaymentLinkDto>()
            {
                IsSuccess=true,
                Message="",
                Data=new ReturnPaymentLinkDto() { PaymentLink= readirectrl }
            });
        }
        [AllowAnonymous]
        [HttpGet("Verify")]
        public IActionResult Verify(Guid paymentId, string callbackUrlFront)
        {
            string Status = HttpContext.Request.Query["Status"];
            string Authority = HttpContext.Request.Query["authority"];
            if (Status != "" & Status.ToString().ToLower() == "ok" && Authority != "")
            {
                var pay = _paymentService.GetPayment(paymentId);
                if (pay == null)
                {
                    return NotFound();
                }
                var client = new RestClient("https://sandbox.zarinpal.com/pg/rest/WebGate/PaymentVerification.json");
                //var client = new RestClient("https://sandbox.zarinpal.com/pg/v4/payment/verify.json");
                var request = new RestRequest("",Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", $"{{\"MerchantID\" :\"{merchendId}\",\"Authority\":\"{Authority}\",\"Amount\":\"{pay.Amount}\"}}", ParameterType.RequestBody);
                var response = client.Execute(request);
                VerificationPayResultDto verification = JsonConvert.DeserializeObject<VerificationPayResultDto>(response.Content);
                if (verification.Status == 100)
                {
                    _paymentService.PayDone(paymentId, Authority, verification.RefID);
                    var message = new PaymentIsDoneMessage()
                    {
                        CreateTime = DateTime.Now,
                        MessageId = Guid.NewGuid(),
                        OrderId = pay.OrderId
                    };
                    _messageBus.SendMessage(message, _queueName);
                    return Redirect(callbackUrlFront);
                }
                else
                {
                    return NotFound(callbackUrlFront);
                }
            }
            return Redirect(callbackUrlFront);
        }

   
    }
}
