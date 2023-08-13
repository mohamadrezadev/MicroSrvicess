using OrdersService.Models.Entites;

namespace OrdersService.Models.Services.OrderServices
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public int ItemCount { get; set; }
        public double TotalPrice { get; set; }
        public bool OrderPaid { get; set; }
        public DateTime OrderPlaced { get; set; }

        public PaymentStatus   PaymentStatus { get; set; }

    }
    
}
