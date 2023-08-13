using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersService.Models.Services.OrderServices;

namespace OrdersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "OrderService.AccessUser")]
    public class OrderPaymentController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderPaymentController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public IActionResult Post(Guid Id)
        {
            return Ok(_orderService.RequestPayment(Id));
        }
    }
}
