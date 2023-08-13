using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersService.Models.Services.OrderServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrdersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "OrderService.AccessUser")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string userid = "1";
            var orders = _orderService.GetOrdersForUser(userid);
            return Ok(orders);
        }
        [HttpGet("{OrderId}")]
        public IActionResult Get(Guid OrderId)
        {
            var order=_orderService.GetOrderById(OrderId);
            return Ok(order);
        }


        

      
    }
}
