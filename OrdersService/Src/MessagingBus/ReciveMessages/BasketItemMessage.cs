namespace OrdersService.MessagingBus.ReciveMessages
{
    public class basketItemMessages
    {
        public Guid BasketItemId { get; set; }
        public Guid ProductId { get; set; }
        public string NameProduct { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
    public class BasketItem
    {
        public string BasketItemId { get; set; }
        public Guid ProductId { get; set; }
        public string NameProduct { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }

}

