using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PaymentService.Application.Service.PaymentServices;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.MessagingBus.RecivePaymentMessage
{
    public class RecivedMessagePaymentForOrder : BackgroundService
    {

        private readonly string _hostName;
        private readonly string _Usename;
        private readonly string _Password;
        private readonly string _queueName;
        private readonly IPaymentService paymentService;
        private IConnection _connection;
        private IModel _channel;
        public RecivedMessagePaymentForOrder(IOptions<RabbitMqConfiguration> options, IPaymentService paymentService)
        {
            this.paymentService = paymentService;
            _hostName = options.Value.HostName;
            _Usename = options.Value.UserName;
            _Password = options.Value.Password;
            _queueName = options.Value.QueueName_OrderSendToPayment;
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
            var Consumer = new EventingBasicConsumer(_channel);
            Consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = JsonConvert.DeserializeObject<MessagePaymentDto>(content);

                var resultHandelMessage = HandelMessage(message.OrderId, message.Amount);
                if (resultHandelMessage)
                    _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(_queueName, false, Consumer);
            return Task.CompletedTask;
        }

        private bool HandelMessage(Guid OrderId, double Amount)
        {
            return paymentService.CreatePayment(OrderId, Amount);

        }
    }
}
