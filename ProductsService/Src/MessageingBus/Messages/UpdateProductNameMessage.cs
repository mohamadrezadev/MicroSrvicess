using Microsoft.AspNetCore.DataProtection;

namespace ProductsService.MessageingBus.Messages
{
    public class UpdateProductNameMessage : BaseMessage
    {
        public Guid ProductId { get; set; }
        public string NewName { get; set; }
        public double Price { get; set; }
    }
}
