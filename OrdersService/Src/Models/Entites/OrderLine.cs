namespace OrdersService.Models.Entites
{
    public class OrderLine
    {
        public Guid Id { get; set; }     
        public int Quantity { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }


    }
}
