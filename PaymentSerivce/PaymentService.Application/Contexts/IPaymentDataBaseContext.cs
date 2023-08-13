using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Orders;
using PaymentService.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Contexts
{
    public interface IPaymentDataBaseContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<Payment> Payments { get; set; }
        int SaveChanges();
    }
}
