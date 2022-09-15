using Microsoft.AspNetCore.Authentication.Cookies;
using WebUI.Handler;
using WebUI.Services;
using WebUI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IdentityTokenHandler>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>
{
    opts.LoginPath = "/Account/Login";
    opts.ExpireTimeSpan = TimeSpan.FromDays(60);
    opts.SlidingExpiration = true;
    opts.Cookie.Name = "NorthwindCookie";
});

builder.Services.AddHttpClient<INorthwindService, NorthwindService>(opt =>
{
    opt.BaseAddress = new Uri("http://localhost:5000/northwindtest/");
}).AddHttpMessageHandler<IdentityTokenHandler>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
