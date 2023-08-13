namespace OrdersService.MessagingBus.ReciveMessages
{

    public class BasketDto
    {
        public string BasketId { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string UserId { get; set; }
        public double TotalPrice { get; set; }
        public List<BasketItem> basketItem { get; set; } = new List<BasketItem>();
        public string MessageId { get; set; }
        public DateTime Creationtime { get; set; }
    }

    //public class BasketDto
    //{
    //    public string BasketId { get; set; }
    //    public string UserId { get; set; }
    //    public string FirsName { get; set; }
    //    public string LastName { get; set; }
    //    public string PhoneNumber { get; set; }
    //    public string Address { get; set; }
    //    public string PostalCode { get; set; }
    //    public double TotalPrice { get; set; }
    //    public List<basketItemMessages> basketItemMessages { get; set; }
    //    public string MessageId { get; set; }
    //    public DateTime CreateTime { get; set; }
    //}
}
