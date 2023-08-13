using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Service.PaymentServices
{
    public interface IPaymentService
    {
        PaymentDto GetPaymentofOrder(Guid OrderId);
        PaymentDto GetPayment(Guid PaymentId);
        bool CreatePayment(Guid OrderID, double Amount);
        void PayDone(Guid PaymentId, string Authority, long RefId);

    }
}
