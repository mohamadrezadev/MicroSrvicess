namespace OrdersService.Models.Entites
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; }
    }
}
