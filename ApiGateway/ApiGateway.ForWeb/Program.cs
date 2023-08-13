using ApiGateway.ForWeb.Models.DiscountServices;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;

namespace ApiGateway.ForWeb
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
            builder.Services.AddScoped<IDiscountService, ApiGateway.ForWeb.Models.DiscountServices.DiscountService>();
            IConfiguration configuration = builder.Configuration;
            IWebHostEnvironment webHostEnvironment = builder.Environment;
            builder.Configuration.SetBasePath(webHostEnvironment.ContentRootPath)
                .AddJsonFile("ocelot.json")
                .AddOcelot(webHostEnvironment)
                .AddEnvironmentVariables();
            builder.Services.AddOcelot(configuration)
                .AddPolly()
                 .AddCacheManager(x =>
                 {
                     x.WithDictionaryHandle();
                 }); ;
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();


            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllers(); 
            });
            app.UseOcelot().Wait();

            app.Run();
        }
    }
}
