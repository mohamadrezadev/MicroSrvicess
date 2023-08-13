namespace OrdersService.Models.Services.OrderServices
{
    public class OrderLineDto
    {
        public Guid Id { get; set; }
      
        public int Quantity { get; set; }
        public string   ProductName { get; set; }
        public double Price { get; set; }
    }
}
