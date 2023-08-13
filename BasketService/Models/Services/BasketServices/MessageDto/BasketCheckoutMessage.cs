using BasketService.MessagingBus;

namespace BasketService.Models.Services.BasketServices.MessageDto
{
    public class BasketCheckoutMessage:BaseMessage
    {
        public Guid BasketId { get; set; }

        public string UserId { get; set; }
        
        public string FirsName { get; set; }
        
        public string LastName { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Address { get; set; }
        
        public string PostalCode { get; set; }

        public double TotalPrice { get; set; }
        public List<BasketItemMessage>  basketItem{ get; set; }=new List<BasketItemMessage>();

    }
    public class BasketItemMessage
    {
        public Guid BasketItemId { get; set; }
        public Guid ProductId { get; set; }
        public string NameProduct { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

    }
}
