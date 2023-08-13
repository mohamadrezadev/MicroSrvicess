using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace PaymentService.Infrastructure.MessagingBus.SendPaymentMessage
{
    public class RabbitMQMessageBus : IMessageBus
    {
        private readonly string _hostName;
        private readonly string _Usename;
        private readonly string _Password;
        private readonly string _queueName;
        private IConnection _connection;
        public RabbitMQMessageBus(IOptions<RabbitMqConfiguration> options)
        {
            _hostName = options.Value.HostName;
            _Usename = options.Value.UserName;
            _Password = options.Value.Password;
            _queueName = options.Value.QueueName_PaymentDone;
            CreateRabbitMQConnection();
        }
        public void SendMessage(BaseMessage message, string QueueName)
        {
            if (CneckRabbitMQConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                    var json = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(json);
                    var Properties = channel.CreateBasicProperties();
                    Properties.Persistent = true;
                    channel.BasicPublish(exchange: "", routingKey: QueueName, basicProperties: Properties, body: body);
                }
            }
        }
        private void CreateRabbitMQConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    UserName = _Usename,
                    Password = _Password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"can not create connection: {ex.Message}");
            }
        }

        private bool CneckRabbitMQConnection()
        {
            if (_connection != null)
            {
                return true;
            }
            CreateRabbitMQConnection();
            return _connection != null;
        }
    }

}
