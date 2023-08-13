namespace BasketService.MessagingBus
{
    public class BaseMessage
    {
        public Guid MessageId { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
