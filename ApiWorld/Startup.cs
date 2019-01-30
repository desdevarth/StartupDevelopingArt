using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using H_Sport.Models;
using H_SportServices.Intefaces;
using H_SportServices.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApiWorld
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var h_SportConnectionString = "Data Source=.;Initial Catalog=H_Plus_Sports;Integrated Security=True";
            services.AddDbContext<H_Plus_SportsContext>(options => options.UseSqlServer(h_SportConnectionString));

            
            services.AddMemoryCache(option=> new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(5)));
            services.AddResponseCaching();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseResponseCaching();
            app.UseHttpsRedirection();
            //app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();
            app.UseMvc(routes=>routes.MapRoute("default", "[controller]/[action]/{id}"));
        }
    }
}
