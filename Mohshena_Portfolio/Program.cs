using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mohshena_Portfolio.Data;
using Mohshena_Portfolio.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<PortfolioDBContext>(opt =>
 opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));  //for postgre sql

/*opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));*/   //register dbcontext

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<PortfolioDBContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

builder.Services.AddFileUploader();

//RENDER-SPECIFIC CONFIGURATION 
// 1. Read the PORT environment variable (Render sets this)
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
// 2. Bind to 0.0.0.0 (all interfaces) on that port
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.SeedAdmin(services);
}




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
