namespace PaymentService.Infrastructure.MessagingBus.RecivePaymentMessage
{
    public class MessagePaymentDto
    {
        public Guid OrderId { get; set; }
        public double Amount { get; set; }
    }
}
