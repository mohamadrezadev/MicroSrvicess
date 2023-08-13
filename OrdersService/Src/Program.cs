using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using OrdersService.Infrastructure.Contexts;
using OrdersService.Infrastructure.MappingProfile;
using OrdersService.MessagingBus.Config;
using OrdersService.MessagingBus.ReciveMessages;
using OrdersService.MessagingBus.SendMessage;
using OrdersService.Models.Services.OrderServices;
using OrdersService.Models.Services.ProductServices;
using OrdersService.Models.Services.RegisterOrderServices;
using RestSharp;

namespace OrdersService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<OrderDatabaseContext>(p =>
            {
                p.UseSqlite(builder.Configuration.GetConnectionString("OrderConnection"));
                

            }, ServiceLifetime.Singleton);
            builder.Services.AddScoped<IOrderService,OrderService>();
            builder.Services.AddTransient<IProductService,ProductService>();
            builder.Services.AddTransient<IRegisterOrderService, RegisterOrderService>();
            builder.Services.AddTransient<IMessage, RabbitMQMessageBus>();
            builder.Services.AddAutoMapper(typeof(OrderMappingProfile));
            builder.Services.AddScoped<IVerifyProductService>(op =>
            {
                var options = new RestClientOptions
                {
                    MaxTimeout = -1,
                    BaseUrl = new Uri(builder.Configuration["MicroServiceAddress:Product:Uri"])

                };
                var restClient = new RestClient(options);
                return new VerifyProductService(restClient);
            });
            builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMq"));
            builder.Services.AddHostedService<ReciveOrderCreateMessage>();
            builder.Services.AddHostedService<RecivePaymentofOrderService>();
            builder.Services.AddHostedService<ReciveProductUpdateMessages>();
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(option =>
            //    {
            //        option.Authority = "https://localhost:7007";
            //        option.Audience = "orderservice";
            //    });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(op =>
                {
                    op.Authority = "https://localhost:7100";
                    op.Audience = "OrderService";
                });

           
           builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("OrderService.AccessUser", policy =>
               policy.RequireClaim("scope", "OrderService.AccessUser"));
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("OrderService.FullAccess", policy =>
               policy.RequireClaim("scope", "OrderService.FullAccess"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}