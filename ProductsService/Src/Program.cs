using Microsoft.EntityFrameworkCore;
using ProductsService.Infrastructure.Contexts;
using ProductsService.MessageingBus.Config;
using ProductsService.MessageingBus.SendMessage;
using ProductsService.Models.Services;

namespace ProductsService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseIISIntegration().UseIIS();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ProductsDatabaseContext>(p =>
            {
                p.UseSqlServer(builder.Configuration.GetConnectionString("ProductConnection"));
            });
            builder.Services.AddTransient<IProductService,ProductService>();
            builder.Services.AddTransient<ICategoryService,CategoryService>();
            builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMq"));
            builder.Services.AddScoped<IMessagesBus, RabbitMQMessagesBus>();


            builder.Services.Configure<IISServerOptions>(op =>
            {
                op.AllowSynchronousIO = true;
            });
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