using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.MessagingBus.SendPaymentMessage
{
    public interface IMessageBus
    {
        void SendMessage(BaseMessage message, string QueueName);
    }

}
