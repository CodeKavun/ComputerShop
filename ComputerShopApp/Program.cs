using ComputerShopApp.Data;
using ComputerShopApp.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("LocalDb")
    ?? throw new InvalidOperationException("There is no such connection string!");
builder.Services.AddDbContext<ShopContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<ShopUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<ShopContext>();

builder.Services.AddAutoMapper(typeof(ShopUserProfile), typeof(RoleProfile));

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseDefaultFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
