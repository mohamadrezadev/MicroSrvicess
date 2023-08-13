namespace OrdersService.MessagingBus.SendMessage
{
    public class BaseMessage
    {
        public Guid MessageId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
