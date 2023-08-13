namespace OrdersService.MessagingBus.Config
{
    public class RabbitMqConfiguration
    {
        public string HostName { get; set; }
        public string QueueName_BaksetChekout { get; set; }
        public string QueueName_OrderSendToPayment { get; set; }
        public string QueueName_PaymentDone { get; set; }
        public string ExchangeName_UpdateProduct { get; set; }
        public string QueueName_GetMessageonUpdateProduct { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
