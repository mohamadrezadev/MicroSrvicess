namespace Microservice.Web.FrontEnd.Services.BasketService
{
    public class BaketItem
    {
        public string id { get; set; }
        public string productid { get; set; }
        public string productName { get; set; }
        public int unitPrice { get; set; }
        public int quantity { get; set; }
        public string imageUrl { get; set; }
        public int TotalPrice()
        {
            return quantity * unitPrice;
        }
    }


}
