using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using first_rest_api.Models;
using first_rest_api.Utilities;
using System;

namespace first_rest_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
              

        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*services.AddDbContext<TodoContext>(opt =>
               opt.UseInMemoryDatabase("TodoList")); */
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

             Console.WriteLine("Content Root Path : "+ env.ContentRootPath);
             Console.WriteLine("Dev Environment : "+ env.IsDevelopment());

            var cb = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json")
               .Build();

           Configuration = cb;

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           // app.UseStaticFiles();
           Constants.SetConnectionString(Configuration.GetConnectionString("dafultConnection"));
            
        //    Constants.SetConnectionString(Configuration["ConnectionStrings::DefaultConnection"]);

        }
    }
}
