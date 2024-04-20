using Microsoft.OpenApi.Models;
using MultiTenantReactApp.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

// Add the TenantService
builder.Services.AddSingleton<TenantService>();
// Add the Tenant Assets Service
builder.Services.AddSingleton<TenantAssetsService>();


// Inside ConfigureServices method
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Multi Tenant API Service", Version = "v1" });
    
});

var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Inside Configure method, before app.UseEndpoints(...)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Multi Tenant API Service v1");
    // Configure Swagger UI to support file uploads
    c.ConfigObject.AdditionalItems["syntaxHighlight"] = false;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
