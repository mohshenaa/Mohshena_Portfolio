using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mohshena_Portfolio.Data;
using Mohshena_Portfolio.Services;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"=== DEBUG: Connection string length = {connString?.Length ?? 0} ===");
if (!string.IsNullOrEmpty(connString) && connString.Length > 0)
{
    Console.WriteLine($"=== DEBUG: First 30 chars = {connString.Substring(0, Math.Min(30, connString.Length))} ===");
}
else
{
    Console.WriteLine("=== DEBUG: Connection string is NULL or EMPTY! ===");
}

// Add services
builder.Services.AddControllersWithViews();

// Database - PostgreSQL
builder.Services.AddDbContext<PortfolioDBContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<PortfolioDBContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

// Custom services
builder.Services.AddFileUploader();

// Render: Bind to dynamic port
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PortfolioDBContext>();
    await dbContext.Database.MigrateAsync();
}

// Seed database (async safe)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.SeedAdmin(services);
}

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Only redirect to HTTPS in development (Render handles HTTPS at load balancer)
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();