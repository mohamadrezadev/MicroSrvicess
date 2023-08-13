namespace Microservice.Web.FrontEnd.Services.BasketService
{
    public class AddToBasketDto
    {
        public string basketId { get; set; }
        public string productid { get; set; }
        public string productName { get; set; }
        public int unitPrice { get; set; }
        public int quantity { get; set; }
        public string imageUrl { get; set; }
    }



}
