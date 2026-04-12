using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mohshena_Portfolio;
using Mohshena_Portfolio.Data;
using Mohshena_Portfolio.Models;
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

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<PhotoService>();

// Custom services
builder.Services.AddFileUploader();

// ❌ REMOVE the UseUrls lines – let launchSettings.json or environment variables handle ports
// No port hardcoding!

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PortfolioDBContext>();
    await dbContext.Database.MigrateAsync();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.SeedAdmin(services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

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