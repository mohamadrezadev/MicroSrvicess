using Microservice.Web.FrontEnd.Services.BasketService;
using Microservice.Web.FrontEnd.Services.DiscountService;
using Microservice.Web.FrontEnd.Services.OrderServices;
using Microservice.Web.FrontEnd.Services.PaymentServices;
using Microservice.Web.FrontEnd.Services.ProductService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using RestSharp;

namespace Microservice.Web.FrontEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IProductService>(p =>
            {
                var options = new RestClientOptions
                {
                    MaxTimeout = -1,
                    BaseUrl=new Uri(builder.Configuration["MicroServiceAddress:ApiGateway:Uri"])

                };
                var restClient = new RestClient(options);
                return new ProductService(restClient);
              
            });

            builder.Services.AddScoped<IBasketSerivce>(p =>
            {
                
                var options = new RestClientOptions
                {
                    MaxTimeout = -1,
                    BaseUrl = new Uri(builder.Configuration["MicroServiceAddress:ApiGateway:Uri"])

                };
                var restClient = new RestClient(options);
                return new BasketSerivce(restClient);

            });
            builder.Services.AddScoped<IOrderService>(p =>
            {
                var options = new RestClientOptions
                {
                    MaxTimeout = -1,
                    BaseUrl = new Uri(builder.Configuration["MicroServiceAddress:ApiGateway:Uri"])
                   // BaseUrl = new Uri("https://localhost:7231")

                };
                var restClient = new RestClient(options);
                return new OrderService(restClient,new HttpContextAccessor());
            });
            builder.Services.AddScoped<IPaymentService>(p =>
            {
                var options = new RestClientOptions
                {
                    MaxTimeout = -1,
                    BaseUrl = new Uri(builder.Configuration["MicroServiceAddress:ApiGateway:Uri"])

                };
                var restClient = new RestClient(options);
                return new PaymentService(restClient);
            });
            builder.Services.AddScoped<IDiscountService>(op =>
            {
                var options = new RestClientOptions
                {
                    MaxTimeout = -1,
                    BaseUrl = new Uri(builder.Configuration["MicroServiceAddress:ApiGateway:Uri"])

                };
                var restClient = new RestClient(options);
                return new DiscountServiceResuFull(restClient);
            });

            builder.Services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, option =>
            {
                option.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.Authority = "https://localhost:7100";
                option.ClientId = "webfrontenduser";
                option.ClientSecret = "123456";
                option.ResponseType = "code";
                option.GetClaimsFromUserInfoEndpoint = true;
                option.SaveTokens = true;
                option.Scope.Add("openid");
                option.Scope.Add("profile");
                option.Scope.Add("OrderService.AccessUser");
                option.Scope.Add("BasketService.AccessUser");
            });

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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}