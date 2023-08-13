using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.MessagingBus
{
    public class RabbitMqConfiguration
    {
        public string HostName { get; set; }
        public string QueueName_PaymentDone { get; set; }
        public string QueueName_OrderSendToPayment { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
