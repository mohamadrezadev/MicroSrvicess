using BasketService.Models.Services.ProductServices;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace BasketService.MessagingBus.RecivedMessages.ProductMessages
{
    public class ProductUpdateMessages : BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private readonly string _queueName;
        private readonly string _exchangeName;
        private readonly IProductService productService;
        public ProductUpdateMessages(IOptions<RabbitMqConfiguration> options,IProductService productService)
        {
           this.productService = productService;
            _hostname = options.Value.HostName;
            _username = options.Value.UserName;
            _password = options.Value.Password;
            _exchangeName = options.Value.ExchangeName_UpdateProduct;
            _queueName = options.Value.QueueName_GetMessageonUpdateProduct;
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                Password = _password,
                UserName = _username
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange:_exchangeName, ExchangeType.Fanout,true,false);
            _channel.QueueDeclare(_queueName,true,false,false);
            _channel.QueueBind(_queueName, _exchangeName, "", null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var updateCustomerFullNameModel = JsonConvert.DeserializeObject<UpdateProductNameMessage>(content);

                var resultHandeleMessage = HandleMessage(updateCustomerFullNameModel);
                if (resultHandeleMessage)
                    _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(_queueName, false, consumer);
            return Task.CompletedTask;
        }
        private bool HandleMessage(UpdateProductNameMessage message)
        {
            return productService.UpdateProduct(message.ProductId, message.NewName, message.Price);
        }
    }
    public class UpdateProductNameMessage
    {
        public Guid ProductId { get; set; }
        public string NewName { get; set; }
        public double Price { get; set; }
    }
}
