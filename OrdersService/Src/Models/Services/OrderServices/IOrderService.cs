using OrdersService.Models.Entites.Dtos;

namespace OrdersService.Models.Services.OrderServices
{
    public interface IOrderService
    {
       
        List<OrderDto> GetOrdersForUser(string UserId);
        OrderDetailDto GetOrderById(Guid Id);
        ResultDto RequestPayment(Guid OrderId);
    }
}
