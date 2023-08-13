namespace PaymentService.Application.Service.PaymentServices
{
    public class PaymentDto
    {
        public Guid PaymentId { get; set; }
        public Guid OrderId { get; set; }
        public double Amount { get; set; }
        public bool IsPay { get; set; }
    }
}
