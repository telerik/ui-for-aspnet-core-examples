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
using Telerik.Examples.Mvc.Hubs;
using Microsoft.AspNetCore.Identity;
using AutoMapper.Internal;

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
                mc.Internal().MethodMappingEnabled = false;
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
                options.ViewLocationFormats.Add("/Views/ImageEditor/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/Editor/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/Forms/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/MultiSelect/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/Scheduler/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/Views/StylesAndLayout/{0}" + RazorViewEngine.ViewExtension);
            });

            services.AddDbContext<GeneralDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

            }).AddEntityFrameworkStores<GeneralDbContext>();

            services.AddKendo();
            services.AddSignalR().AddJsonProtocol(options => {
                options.PayloadSerializerOptions.PropertyNamingPolicy = null;
            });
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MeetingHub>("/meetingHub");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<GeneralDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
