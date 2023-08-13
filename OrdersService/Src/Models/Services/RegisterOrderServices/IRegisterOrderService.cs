using OrdersService.Infrastructure.Contexts;
using OrdersService.MessagingBus.ReciveMessages;
using OrdersService.Models.Entites;
using OrdersService.Models.Services.ProductServices;

namespace OrdersService.Models.Services.RegisterOrderServices
{
    public interface IRegisterOrderService
    {
        bool Execute(BasketDto basketDto);
    }
    public class RegisterOrderService : IRegisterOrderService
    {
        private readonly IProductService _productService;
        private readonly OrderDatabaseContext _Dbcontext;

        public RegisterOrderService(IProductService productService,OrderDatabaseContext orderDatabaseContext)
        {
            _productService = productService;
            _Dbcontext = orderDatabaseContext;
        }
        public bool Execute(BasketDto basket)
        {
            List<OrderLine> orderLines = new List<OrderLine>();
            foreach (var basketItem in basket.basketItem)
            {
                var product = _productService.GetProduct(new ProductDto()
                {
                    ProductId = basketItem.ProductId,
                    Price = basketItem.Price,
                    ProductName = basketItem.NameProduct,
                });
                orderLines.Add(new OrderLine
                {
                    Id = Guid.NewGuid(),
                    Quantity = basketItem.Quantity,
                    ProductId = product.ProductId,
                    Product = product,
                   

                });
            }
            Order order = new Order(basket.UserId,basket.FirsName, basket.LastName,
                basket.Address,basket.PhoneNumber,basket.TotalPrice,orderLines);
            _Dbcontext.Orders.Add(order);
            _Dbcontext.SaveChanges();
            return true;
            
        }
    }
}
