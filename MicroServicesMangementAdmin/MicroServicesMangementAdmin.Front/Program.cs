using MicroServicesMangementAdmin.Front.ViewServices.ProductServices;
using RestSharp;

namespace MicroServicesMangementAdmin.Front
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IProductManagementService>(p => {
                var options = new RestClientOptions
                {
                    MaxTimeout = -1,
                    BaseUrl = new Uri(builder.Configuration["MicroSerivcessAddres:ProductService:Uri"])

                };
                var restClient = new RestClient(options);
                return new ProductManagementService(restClient);
            });
              //  ProductManagementService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
