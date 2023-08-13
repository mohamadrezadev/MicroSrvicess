using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrdersService.MessagingBus.Config;
using OrdersService.Models.Services.ProductServices;
using OrdersService.Models.Services.RegisterOrderServices;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;

namespace OrdersService.MessagingBus.ReciveMessages
{
    public class ReciveOrderCreateMessage : BackgroundService
    {
        private readonly IRegisterOrderService _registerOrderService;
        private readonly string _hostName;
        private readonly string _Usename;
        private readonly string _Password;
        private readonly string _queueName;
        private IConnection _connection;
        private IModel _channel;
        public ReciveOrderCreateMessage(IOptions<RabbitMqConfiguration> options,IRegisterOrderService registerOrderService)
        {
            _registerOrderService = registerOrderService;
            _hostName = options.Value.HostName;
            _Usename = options.Value.UserName;
            _Password = options.Value.Password;
            _queueName = options.Value.QueueName_BaksetChekout;
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _Usename,
                Password = _Password
                
            };
            _connection=factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        }
       
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var Cunsumer = new EventingBasicConsumer(_channel);
            Cunsumer.Received += (sender, eventArg) =>
            {
                //var body = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                var body = Encoding.UTF8.GetString(eventArg.Body.ToArray());
                var Basket = JsonConvert.DeserializeObject<BasketDto>(body);
            


                //ثبت سفارش 
                var resultHandel= HandelMessage(Basket);
                if (resultHandel)
                _channel.BasicAck(eventArg.DeliveryTag, false);
            };
            _channel.BasicConsume(queue:_queueName,autoAck:false,consumer:Cunsumer);

            return Task.CompletedTask;
        }
        private bool HandelMessage(BasketDto basket)
        { 
           return _registerOrderService.Execute(basket);
       
        }
    }
}
