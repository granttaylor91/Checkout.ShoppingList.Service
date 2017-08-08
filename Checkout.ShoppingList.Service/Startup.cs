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
using Checkout.ShoppingList.Service.Attributes;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

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

            services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
            services.AddMvc(config => {
                config.Filters.Add(new CustomExceptionFilterAttribute());
         
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Drinks Shopping List Service",
                    Version = "v1",
                    Description = "Very basic API for Checkout.com Technical Assignment.",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                    {
                        Name = "Grant Taylor",
                        Email = "GrantTaylor91@gmail.com"
                    }
                });
                //Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Checkout.ShoppingList.Service.xml");
                c.IncludeXmlComments(xmlPath);

            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Drinks Shoppinglist Service v1");
            });
        }
    }
}
