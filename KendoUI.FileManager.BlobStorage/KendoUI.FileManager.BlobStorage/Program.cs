using Azure.Storage.Blobs;
using KendoUI.FileManager.BlobStorage.Services;
using Kendo.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddKendo(x =>
{
    x.DeferToScriptFiles = true;
});

// Add Azure Blob Storage service
var connectionString = builder.Configuration.GetConnectionString("AzureStorage") ??
                      "UseDevelopmentStorage=true"; // Default to Azurite for development
builder.Services.AddSingleton(x => new BlobServiceClient(connectionString));
builder.Services.AddSingleton<IBlobFileManagerService, BlobFileManagerService>();

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

app.UseAuthorization();

app.UseMiddleware<KendoDeferredScriptsMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();