namespace ProductsService.MessageingBus.Messages
{
    public class BaseMessage
    {
        public Guid MessageId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
