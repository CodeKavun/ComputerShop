using ComputerShopApp.Data;
using ComputerShopApp.Profiles;
using ComputerShopApp.Requirements;
using Microsoft.AspNetCore.Authorization;
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

builder.Services.AddScoped<IAuthorizationRequirement, MinimalAgeRequirement>();
builder.Services.AddScoped<IAuthorizationHandler, MinimalAgeAuthorizationHandler>();

builder.Services.AddAuthentication().AddGoogle(options =>
{
    IConfigurationSection googleSection = builder.Configuration.GetSection("Authentication:Google");
    string clientId = googleSection.GetValue<string>("ClientId")!;
    string clientSecret = googleSection.GetValue<string>("ClientSecret")!;

    options.ClientId = clientId;
    options.ClientSecret = clientSecret;
});
builder.Services.AddAuthorization(configure =>
{
    configure.AddPolicy("managerPolicy", policyBuilder =>
    {
        policyBuilder.RequireRole("manager");
        policyBuilder.RequireAuthenticatedUser();
    });
    configure.AddPolicy("isMore18YO", policyBuilder =>
    {
        policyBuilder.RequireRole("mananger");
        policyBuilder.Requirements.Add(new MinimalAgeRequirement());
    });
});

builder.Services.AddAutoMapper(typeof(ShopUserProfile), typeof(RoleProfile), typeof(CategoryProfile), typeof(ProductImageProfile));

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    await BrandSeeder.Seed(scope.ServiceProvider, app.Environment);
    await CategorySeeder.Seed(scope.ServiceProvider, app.Environment);
}

app.UseStaticFiles();
app.UseDefaultFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

app.Run();
