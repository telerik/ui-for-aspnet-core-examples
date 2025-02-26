using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;
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
using Telerik.Examples.Mvc.Database;
using Telerik.Examples.Mvc.Seeders;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using Kendo.Mvc.Extensions;
using System.Collections.Generic;
using System.Threading;
using Telerik.SvgIcons;


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
    options.ViewLocationFormats.Add("/Views/ListBox/{0}" + RazorViewEngine.ViewExtension);
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

builder.Services.AddDbContext<InMemoryDbContext>(options =>
    options.UseInMemoryDatabase("TelerikCoreDb")
);

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
    .AddSession(opts =>
    {
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

app.MapGet($"/api/minimalapi", (MinimalAPIDataSourceRequest request) =>
{
    var data = MinimalAPIDataSourceRequest.GetData();

    var dataSourceRequest = new DataSourceRequest
    {
        Page = request.Page,
        PageSize = request.PageSize,
        Aggregates = request.Aggregates,
        Filters = request.Filters,
        Groups = request.Groups,
        Sorts = request.Sorts
    };

    return Results.Json(data.ToDataSourceResult(dataSourceRequest), new System.Text.Json.JsonSerializerOptions
    {
        PropertyNamingPolicy = null
    });
})
.WithName("GetOrders");

app.MapPost("/api/minimalapi", ([FromForm] OrderViewModel model) =>
{
    // Perform Database Create Operation

    var result = new DataSourceResult
    {
        Data = new[] { model },
        Total = 1
    };

    return Results.Json(result, new JsonSerializerOptions
    {
        PropertyNamingPolicy = null
    });
})
.DisableAntiforgery();

app.MapPut("/api/minimalapi/{id}", ([FromForm] OrderViewModel model, int id) =>
{
    // Perform Database Update Operation

    return Results.NoContent();
})
.DisableAntiforgery();

app.MapDelete("/api/minimalapi/{id}", (int id) =>
{
    // Perform Database Delete Operation

    return Results.NoContent();
});

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


using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<GeneralDbContext>();
    context.Database.Migrate();

    var inMemoryContext = serviceScope.ServiceProvider.GetRequiredService<InMemoryDbContext>();
    DataSeeder.SeedListBoxItems(inMemoryContext);
}

app.Run();


static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Product>("Products");
    builder.EntitySet<Category>("Categories");
    return builder.GetEdmModel();
}