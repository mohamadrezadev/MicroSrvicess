namespace BasketService.Models.Services.BasketServices
{
    public class Productdto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public string ImageUrl { get; set; }
    }
}
