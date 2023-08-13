using PaymentService.Application.Contexts;
using PaymentService.Domain.Orders;
using PaymentService.Domain.Payments;

namespace PaymentService.Application.Service.PaymentServices
{
    public class PaymentServiceConcret : IPaymentService
    {
        private readonly IPaymentDataBaseContext context;

        public PaymentServiceConcret(IPaymentDataBaseContext context)
        {
            this.context = context;
        }
        public bool CreatePayment(Guid OrderID, double Amount)
        {
            var order = GetOrder(OrderID, Amount);
            var payment = context.Payments.SingleOrDefault(p => p.OrderId == order.Id);
            if (payment!=null)
            {
                return true;
            }
            else
            {
                var newpayment = new Payment()
                {
                    Amount = Amount,
                    Id = Guid.NewGuid(),
                    IsPay = false,
                    Order= order
                };
                context.Payments.Add(newpayment);
                context.SaveChanges();
                return true;
            }
        }
        private Order GetOrder(Guid OrderID, double Amount)
        {
            var order = context.Orders.SingleOrDefault(p => p.Id == OrderID);
            if (order != null)
            {
                if (order.Amount != Amount)
                {
                    order.Amount = Amount;
                    context.SaveChanges();
                }
                return order;
            }
            else
            {
                var neworder = new Order()
                {
                    Amount = Amount,
                    Id = OrderID

                };
                context.Orders.Add(neworder);
                context.SaveChanges();
                return neworder;
                
            }
        }
        public PaymentDto GetPayment(Guid PaymentId)
        {
            var payment=context.Payments.FirstOrDefault(p=>p.Id== PaymentId);
            if (payment!=null)
            {
                return new PaymentDto()
                {
                    Amount=payment.Amount,
                    IsPay=payment.IsPay,
                    PaymentId=payment.Id,
                    OrderId=payment.OrderId,

                };
            }
            return null;
        }

        public PaymentDto GetPaymentofOrder(Guid OrderId)
        {
            var payment=context.Payments.SingleOrDefault(p=>p.OrderId== OrderId);
            if (payment != null)
            {
                return new PaymentDto()
                {
                    Amount = payment.Amount,
                    IsPay = payment.IsPay,
                    OrderId = payment.OrderId,
                    PaymentId = payment.Id,
                };
            }
            else
                return null;
        }

        public void PayDone(Guid PaymentId, string Authority, long RefId)
        {
            var payment = context.Payments.SingleOrDefault(p => p.Id == PaymentId);
            payment.DatePay= DateTime.Now;
            payment.RefId = RefId;
            payment.Authority=Authority;
            payment.IsPay=true;
            context.SaveChanges();

        }
    }
}
