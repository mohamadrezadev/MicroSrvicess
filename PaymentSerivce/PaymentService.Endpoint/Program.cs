using Microsoft.EntityFrameworkCore;
using PaymentService.Application.Contexts;
using PaymentService.Application.Service.PaymentServices;
using PaymentService.Infrastructure.MessagingBus;
using PaymentService.Infrastructure.MessagingBus.RecivePaymentMessage;
using PaymentService.Infrastructure.MessagingBus.SendPaymentMessage;
using PaymentService.Persistance.Context;

namespace PaymentService.Endpoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Add services to the container.
            builder.Services.AddTransient<IPaymentDataBaseContext, PaymentDatabaseContext>();
            builder.Services.AddDbContext<PaymentDatabaseContext>(o =>
            {
                o.UseSqlite(builder.Configuration.GetConnectionString("PaymentConnection"));
            },ServiceLifetime.Singleton);

            builder.Services.AddTransient<IPaymentService , PaymentServiceConcret>();
            builder.Services.AddTransient<IMessageBus, RabbitMQMessageBus>();
            builder.Services.AddHostedService<RecivedMessagePaymentForOrder>();
            builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMq"));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}