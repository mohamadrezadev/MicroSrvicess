using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OrdersService.Infrastructure.Contexts;
using OrdersService.MessagingBus.Config;
using OrdersService.MessagingBus.Messages;
using OrdersService.MessagingBus.SendMessage;
using OrdersService.Models.Entites;
using OrdersService.Models.Entites.Dtos;
using System.Collections;

namespace OrdersService.Models.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly OrderDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IMessage _messagebus;
        private readonly string _QueuName;
        public OrderService(OrderDatabaseContext context, IMapper mapper,
            IMessage message,IOptions<RabbitMqConfiguration> options)
        {
            _context = context;
            _mapper = mapper;
            _messagebus = message;
            _QueuName = options.Value.QueueName_OrderSendToPayment;
        }

        public OrderDetailDto GetOrderById(Guid Id)
        {
            var orders = _context.Orders
                .Include(p => p.OrderLines)
                .ThenInclude(p => p.Product)
                .FirstOrDefault(p => p.Id==Id);
            if (orders == null)
                throw new NotImplementedException("order Not found");


            var result = new OrderDetailDto()
            {
                Id = orders.Id,
                Address = orders.Address,
                FirstName = orders.FirstName,
                LastName = orders.LastName,
                PhoneNumber = orders.PhoneNumber,
                OrderPaid = orders.OrderPaid,
                OrderPlaced = orders.OrderPlaced,
                TotalPrice = orders.TotalPrice,
                UserId = orders.UserId,
                PaymentStatus=orders.PaymentStatus,
                OrderLines = orders.OrderLines.Select(ol => new OrderLineDto
                {
                    Id = ol.Id,
                    ProductName = ol.Product.ProductName,
                    Price = ol.Product.Price,
                    Quantity = ol.Quantity
                }).ToList()


            };
            return result;
        }

        public List<OrderDto> GetOrdersForUser(string UserId)
        {
            var orders=_context.Orders
                .Include(p=>p.OrderLines)
                .Where(p=>p.UserId==UserId)
                .Select(p=>new OrderDto
                {
                    Id=p.Id,
                    TotalPrice=p.TotalPrice,
                    ItemCount=p.OrderLines.Count(),
                    OrderPaid=p.OrderPaid,
                    OrderPlaced=p.OrderPlaced,
                    PaymentStatus=p.PaymentStatus,
                }).ToList();
            return orders;
        }

        public ResultDto RequestPayment(Guid OrderId)
         {
            var order = _context.Orders.SingleOrDefault(p => p.Id == OrderId);
            if (order == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "سفارش یافت نشد"
                };
            }

            //send messge with messagebus
            var message = new SendOrderToPaymentMessage()
            {
                OrderId = order.Id,
                Amount = order.TotalPrice,
                CreateTime = DateTime.Now,
                MessageId = Guid.NewGuid(),

            };
            _messagebus.SendMessage(message,_QueuName);

            order.RequestPayment();
          
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "درخواست پرداخت ثبت گردید"
            };
        }
    }

   
}


