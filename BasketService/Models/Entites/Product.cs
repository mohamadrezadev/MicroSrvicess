namespace BasketService.Models.Entites
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string  ProductName { get; set; }
        public double UnitPrice { get; set; }
        public string   ImageUrl   { get; set; }
        public ICollection<BasketItem>  basketItems{ get; set; }
    }
}
