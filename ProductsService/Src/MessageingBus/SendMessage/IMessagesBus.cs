using ProductsService.MessageingBus.Messages;
using System.Text.Json.Serialization;

namespace ProductsService.MessageingBus.SendMessage
{
    public interface IMessagesBus
    {
        void SendMessage(BaseMessage baseMessage);
    }
}
