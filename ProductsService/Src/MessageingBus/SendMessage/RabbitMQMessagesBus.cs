using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProductsService.MessageingBus.Config;
using ProductsService.MessageingBus.Messages;
using RabbitMQ.Client;
using System.Text;

namespace ProductsService.MessageingBus.SendMessage
{
    public class RabbitMQMessagesBus : IMessagesBus
    {
        private readonly string _hostName;
        private readonly string _Usename;
        private readonly string _Password;
        private readonly string _ExchangeName;
        private IConnection _connection;
        public RabbitMQMessagesBus(IOptions<RabbitMqConfiguration> options)
        {
            _hostName = options.Value.HostName;
            _Password=options.Value.Password;
            _Usename = options.Value.UserName;
            _ExchangeName = options.Value.ExchangeName_UpdateProduct;
            CreateRabbitMQConnection();

        }
        public void SendMessage(BaseMessage baseMessage)
        {
            if (CneckRabbitMQConnection())
            {
                using (var channel =_connection.CreateModel())
                {
                    //channel.QueueDeclare(queue: QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                    channel.ExchangeDeclare(_ExchangeName, ExchangeType.Fanout, true, false, null);
                    var json=JsonConvert.SerializeObject(baseMessage);
                    var body = Encoding.UTF8.GetBytes(json);
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    channel.BasicPublish(exchange:_ExchangeName,"",basicProperties:properties,body:body);
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
