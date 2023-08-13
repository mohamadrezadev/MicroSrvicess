using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json.Serialization;

namespace BasketService.MessagingBus
{
    public interface IMessageBus
    {
        void SendMessage(BaseMessage BaseMessage, string QueuName);
    }
    public class RabbitMqMessageBus : IMessageBus
    {
        private readonly string _hostName ;
        private readonly string _Usename ;
        private readonly string _Password ;
        private  IConnection _connection ;
        public RabbitMqMessageBus(IOptions<RabbitMqConfiguration> options)
        {
            _hostName = options.Value.HostName ;
            _Usename=options.Value.UserName ;
            _Password = options.Value.Password ;
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
                _connection=factory.CreateConnection();

            }
            catch (Exception)
            {
                Console.WriteLine($"can Connectio to rabbitmq");
            }
        }
        private bool CheckRabbbitMqConnection()
        {
            if (_connection != null)
            {
                return true;
            }
            CreateRabbitMQConnection();
            return _connection != null;
            //if (_connection != null)
            //{
            //    return true;
            //}
            //return false;
        }
        public void SendMessage(BaseMessage BaseMessage, string QueuName)
        {
            if (CheckRabbbitMqConnection())
            {
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QueuName,durable:true,exclusive:false,autoDelete:false,arguments:null);
                    var json=JsonConvert.SerializeObject(BaseMessage);
                    var body = Encoding.UTF8.GetBytes(json);
                    var Properties = channel.CreateBasicProperties();
                    Properties.Persistent = true;
                    channel.BasicPublish(exchange:"",routingKey:QueuName,basicProperties:Properties,body:body);
                }
            }
        }
    }
}
