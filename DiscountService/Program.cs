using DiscountService.GRPC;
using DiscountService.Infrastructure.Contexts;
using DiscountService.Infrastructure.MappingProfile;
using DiscountService.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace DiscountService
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
            builder.Services.AddDbContext<DiscountDatabaseContext>(p =>
            {
                p.UseSqlite(builder.Configuration.GetConnectionString("DisCountConnection"));
            });
            builder.Services.AddAutoMapper(typeof(DiscountMappingProfile));
            builder.Services.AddTransient<IDiscountService,Models.Services.DiscountService>();
            builder.Services.AddGrpc();
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
            app.MapGrpcService<GRPCDiscountService>();
            app.Run();
        }
    }
}