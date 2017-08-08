using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Checkout.ShoppingList.Data;
using Microsoft.EntityFrameworkCore;
using Checkout.ShoppingList.Data.Model;

namespace Checkout.ShoppingList.Service
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ShoppingListContext>(opt => opt.UseInMemoryDatabase());

            services.AddSingleton<IShoppingListRepository, ShoppingListRepository>();
            services.AddMvc();

        }


        private static void AddTestData(ShoppingListContext context)
        {

            var drinkOrders = new List<DrinkOrder>
            {
                new DrinkOrder { Name="Pepsi", Quantity= 2},
                new DrinkOrder { Name="Coca Cola", Quantity= 1},
                new DrinkOrder { Name="Milk", Quantity = 5}
            };
          
            context.shoppingList.AddRange(drinkOrders);
            context.SaveChanges();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var context = app.ApplicationServices.GetService<ShoppingListContext>();

            AddTestData(context);

            app.UseMvc();
        }
    }
}
