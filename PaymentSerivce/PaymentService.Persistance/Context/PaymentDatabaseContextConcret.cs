using Microsoft.EntityFrameworkCore;
using PaymentService.Application.Contexts;
using PaymentService.Domain.Orders;
using PaymentService.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Persistance.Context
{
    public class PaymentDatabaseContext:DbContext, IPaymentDataBaseContext
    {
        public PaymentDatabaseContext(DbContextOptions<PaymentDatabaseContext> options):base(options)
        { 
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }


    }
}
