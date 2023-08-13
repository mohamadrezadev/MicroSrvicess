using Microservice.Web.FrontEnd.Services.OrderServices;
using Microservice.Web.FrontEnd.Services.PaymentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Web.FrontEnd.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly string UserID = "1";
        public OrderController(IOrderService orderService,IPaymentService paymentService)
        {
            _orderService = orderService;
            _paymentService = paymentService;
        }

        public IActionResult Index()
        {
            var orders=_orderService.GetOrders(UserID);
            return View(orders);
        }
        public IActionResult Details(Guid Id)
        {
            var order = _orderService.GetOrderDetail(Id);
            return View(order);
        }
        public async Task<IActionResult> Pay(Guid OrderId)
        {
            var order=await _orderService.GetOrderDetail(OrderId);
            if (order.paymentStatusenum == PaymentStatus.isPaid)
            {
                return RedirectToAction(nameof(Details), new { Id = OrderId });
            }
            if (order.paymentStatusenum==PaymentStatus.unPaid)
            {
                //send request for order service for pay 
               var paymentRequest = _orderService.RequestPayment(OrderId);
            }
            //get link payment from service payment
            string calbakurl = Url.Action(nameof(Details), "Order", new { Id = OrderId }, protocol: Request.Scheme);
            var paymentlink = _paymentService.GetPaymentLink(OrderId, calbakurl);
            if (paymentlink.IsSuccess )
            {
                return Redirect(paymentlink.Data.PaymentLink);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
