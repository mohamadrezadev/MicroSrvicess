namespace ProductsService.MessageingBus.Config
{
    public class RabbitMqConfiguration
    {
        public string HostName { get; set; }
        public string ExchangeName_UpdateProduct { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
