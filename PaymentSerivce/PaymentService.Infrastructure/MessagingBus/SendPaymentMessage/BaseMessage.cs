namespace PaymentService.Infrastructure.MessagingBus.SendPaymentMessage
{
    public class BaseMessage
    {
        public Guid MessageId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
