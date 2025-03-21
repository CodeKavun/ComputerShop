using ComputerShopDomainLibrary;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopApp.Data
{
    public static class BrandSeeder
    {
        public static async Task Seed(IServiceProvider provider, IWebHostEnvironment environment)
        {
            DbContextOptions<ShopContext> contextOptions = provider.GetRequiredService<DbContextOptions<ShopContext>>();
            using (ShopContext context = new ShopContext(contextOptions))
            {
                if (context.Brands.Any()) return;

                Brand brand1 = new Brand
                {
                    Name = "Lenovo",
                    Country = "China"
                };
                Brand brand2 = new Brand
                {
                    Name = "Dream Machines",
                    Country = "Poland"
                };

                await context.Brands.AddRangeAsync(brand1, brand2);
                await context.SaveChangesAsync();
            }
        }
    }
}
