using BasketService.Infrastructure.Contexts;
using BasketService.Infrastructure.MappingProfile;
using BasketService.MessagingBus;
using BasketService.MessagingBus.RecivedMessages.ProductMessages;
using BasketService.Models.Services.BasketServices;
using BasketService.Models.Services.DiscountServices;
using BasketService.Models.Services.ProductServices;
using Microsoft.EntityFrameworkCore;

namespace BasketService
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
            builder.Services.AddDbContext<BasketDatabaseContext>(p =>
            {
                p.UseSqlite(builder.Configuration.GetConnectionString("BasketConnection"));
            },ServiceLifetime.Singleton);
            builder.Services.AddAutoMapper(typeof(BasketMappingProfile));
            builder.Services.AddTransient<IBasketService,Models.Services.BasketServices.BasketService>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IDiscountService,Models.Services.DiscountServices.DiscountService>();
            builder.Services.AddTransient<IMessageBus,RabbitMqMessageBus>();
            builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMq"));
            builder.Services.AddHostedService<ProductUpdateMessages>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
           
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}