using OrdersService.MessagingBus.SendMessage;

namespace OrdersService.MessagingBus.Messages
{
    public class SendOrderToPaymentMessage:BaseMessage
    {
        public Guid OrderId { get; set; }
        public double Amount { get; set; }
    }
}
