using PaymentService.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domain.Orders
{
    public class Order
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
