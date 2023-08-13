namespace BasketService.Models.Services.BasketServices
{
    public class BasketItemDto
    {
        public Guid id { get; set; }
        public Guid Productid { get; set; }
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }

    }
}
