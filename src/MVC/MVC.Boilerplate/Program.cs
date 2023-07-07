using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Diagnostics;
using MVC.Boilerplate.Middleware;
using Serilog;
using System.Net;

using MVC.Boilerplate.Common.Helpers.ApiHelper;
using System.Text.Json.Serialization;
using ServiceStack.Text;
using Rotativa.AspNetCore;

using MVC.Boilerplate.Common;
using AspNetCoreHero.ToastNotification.Extensions;
using MVC.Boilerplate.Services;
using MVC.Boilerplate.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration Configuration = builder.Configuration;
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
});

builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

//logger setup
Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
builder.Host.UseSerilog(((ctx, lc) => lc
.ReadFrom.Configuration(ctx.Configuration)));


builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped(typeof(IApiClient<>), typeof(ApiClient<>));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILazyService, LazyService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddInfrastructureServices(Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// RotativaConfiguration.Setup(app.Environment);
RotativaConfiguration.Setup((Microsoft.AspNetCore.Hosting.IHostingEnvironment)app.Environment);
app.UseHttpsRedirection();
app.UseNotyf();
app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseSession();
app.UseRouting();
app.UseCustomExceptionHandler();
app.UseAuthMiddleware();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
