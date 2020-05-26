using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Examples.Mvc.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using AutoMapper;

namespace Telerik.Examples.Mvc
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
            services.AddControllersWithViews();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddTransient<CarsService>();
            services.AddMvc()
                .AddNewtonsoftJson(options =>
                       options.SerializerSettings.ContractResolver =
                          new DefaultContractResolver());

            services.Configure<RazorViewEngineOptions>(options => {
                options.ViewLocationFormats.Add("/Views/Grid/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/Editor/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/Forms/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/MultiSelect/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/Scheduler/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/StylesAndLayout/{0}" + RazorViewEngine.ViewExtension);
            });

            var connection = @"Server=(localdb)\mssqllocaldb;Database=Samples;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<GeneralDbContext>
                (options => options.UseSqlServer(connection));

            services.AddKendo();
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<GeneralDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
