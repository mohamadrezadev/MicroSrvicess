using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.Swagger;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;

namespace IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            


            string constr= builder.Configuration.GetConnectionString("mydb");
            //var migassmbly = typeof(Program).Assembly.GetName().Name;
            var miAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

            // Add services to the container.
          
            builder.Services.AddMvcCore();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            builder.Services.AddRazorPages();



            //Duende.IdentityServer.EntityFramework.DbContexts.ConfigurationDbContext
            //Duende.IdentityServer.EntityFramework.DbContexts.PersistedGrantDbContext;
            builder.Services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddTestUsers(new List<Duende.IdentityServer.Test.TestUser>
                {
                    new Duende.IdentityServer.Test.TestUser()
                    {
                        IsActive = true,
                        Password="123456",
                        Username="mohamadreza",
                        SubjectId="1",

                    },

                })
                .AddConfigurationStore(op =>
                {
                    op.ConfigureDbContext = b =>b.UseSqlite(constr, sql => sql.MigrationsAssembly(miAssembly));
                })
                .AddOperationalStore(op =>
                {
                    //for tokens expiers
                    op.ConfigureDbContext = b =>b.UseSqlite(constr, sql => sql.MigrationsAssembly(miAssembly));
                    op.EnableTokenCleanup = true;   
                    
                });
            //.AddInMemoryClients(new List<Client>()
            //{
            //    new Client()
            //    {
            //       ClientName="frontend web",
            //       ClientId="webfrontend",
            //       ClientSecrets={new Secret("123456".Sha256())},
            //       AllowedGrantTypes=GrantTypes.ClientCredentials,
            //       AllowedScopes={"OrderService.FullAccess"},
            //       RequireConsent=true,

            //    },
            //    new Client()
            //    {
            //        ClientName="web frontend code",
            //        ClientId="webfrontenduser",
            //        ClientSecrets={new Secret("123456".Sha256()) },
            //        AllowedGrantTypes=GrantTypes.Code,
            //        RedirectUris={ "https://localhost:7237/signin-oidc" },
            //        PostLogoutRedirectUris={ "https://localhost:7237/signout-callback-oidc" },
            //        AllowedScopes={"openid","profile", "OrderService.AccessUser", "BasketService.AccessUser" },
            //         RequireConsent=true,
            //    },
            //    new Client()
            //    {
            //        ClientName="Admin",
            //        ClientId="webfrontPalnelAdmin",
            //        ClientSecrets={new Secret("admin".Sha256()) },
            //        AllowedGrantTypes=GrantTypes.Code,
            //        RedirectUris={ "https://localhost:7237/signin-oidc" },
            //        PostLogoutRedirectUris={ "https://localhost:7237/signout-callback-oidc" },
            //        AllowedScopes={"openid","profile", "OrderService.FullAccess", "BasketService.FullAccess" },
            //         RequireConsent=true,

            //    }

            //})

            //.AddInMemoryIdentityResources(new List<IdentityResource> 
            //{
            //    new IdentityResources.OpenId(),
            //    new IdentityResources.Profile()
            //})
            //.AddInMemoryApiScopes(new List<ApiScope>
            //{
            //    new ApiScope("OrderService.AccessUser"),
            //    new ApiScope("OrderService.FullAccess"),
            //    new ApiScope("BasketService.FullAccess"),
            //    new ApiScope("BasketService.AccessUser")
            //}).AddInMemoryApiResources(new List<ApiResource>
            //{

            //    new ApiResource("OrderService","Order Services api")
            //    {
            //        Scopes={ "OrderService.FullAccess", "OrderService.AccessUser" }

            //    } ,
            //    new ApiResource("BasketService","Basket Services api")
            //    {
            //        Scopes={ "BasketService.FullAccess" , "BasketService.AccessUser" }
            //    }
            //});

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI();
         
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.MapControllers();
            app.MapRazorPages();
           
            app.Run();
        }
    }
}