using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrdersService.Infrastructure.Contexts;
using OrdersService.MessagingBus.Config;
using OrdersService.Models.Services.RegisterOrderServices;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace OrdersService.MessagingBus.ReciveMessages
{
    public class RecivePaymentofOrderService : BackgroundService
    {
        
        private readonly string _hostName;
        private readonly string _Usename;
        private readonly string _Password;
        private readonly string _queueName;
        private readonly OrderDatabaseContext _Context;
        private IConnection _connection;
        private IModel _channel;
        public RecivePaymentofOrderService(IOptions<RabbitMqConfiguration> options,OrderDatabaseContext orderDatabaseContext)
        {
          
            _hostName = options.Value.HostName;
            _Usename = options.Value.UserName;
            _Password = options.Value.Password;
            _queueName = options.Value.QueueName_PaymentDone;
            _Context = orderDatabaseContext;
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _Usename,
                Password = _Password

            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var paumentDone = JsonConvert.DeserializeObject<PaymentOrderMessage>(content);
                var resultHandelMessage = HandelMessage(paumentDone);
                if (resultHandelMessage)
                {
                    _channel.BasicAck(ea.DeliveryTag, false);
                }

            };
            _channel.BasicConsume(_queueName, false, consumer);
            return Task.CompletedTask;
        }
        private bool HandelMessage(PaymentOrderMessage paymentOrderMessage)
        {
            var order = _Context.Orders.SingleOrDefault(p => p.Id == paymentOrderMessage.OrderId);
            if (order != null)
            {
                order.PaymentIsDone();
                _Context.SaveChanges();
                return true;
            }
            return false;

        }
    }
    public class PaymentOrderMessage
    {
        public Guid OrderId { get; set; }
    }
}
