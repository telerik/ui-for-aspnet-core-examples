using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Telerik.Examples.Mvc.Hubs;
using AutoMapper;
using AutoMapper.Internal;
using Telerik.Examples.Mvc.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddControllers().AddOData(options =>
{
    options.AddRouteComponents("odata", GetEdmModel());
    options.Select()
           .Filter()
           .Count()
           .OrderBy()
           .Expand()
           .Select()
           .SetMaxTop(null);
});

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.Internal().MethodMappingEnabled = false;
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddTransient<CarsService>();

builder.Services.AddMvc()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ContractResolver = new DefaultContractResolver());

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Add("/Views/Captcha/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/Grid/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/ImageEditor/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/Editor/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/Forms/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/TreeList/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/MultiSelect/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/Scheduler/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/StylesAndLayout/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/Spreadsheet/{0}" + RazorViewEngine.ViewExtension);
});

builder.Services.AddDbContext<GeneralDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<GeneralDbContext>();

builder.Services.AddKendo();

builder.Services.AddSignalR()
    .AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services
    .AddDistributedMemoryCache()
    .AddSession(opts => {
        opts.Cookie.IsEssential = true;
    });

builder.Services.Configure<IISServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<MeetingHub>("/meetingHub");
    endpoints.MapHub<GridHub>("/gridHub");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

using var serviceScope = app.Services.CreateScope();
var context = serviceScope.ServiceProvider.GetRequiredService<GeneralDbContext>();
context.Database.Migrate();

app.Run();


static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Product>("Products");
    builder.EntitySet<Category>("Categories");
    return builder.GetEdmModel();
}