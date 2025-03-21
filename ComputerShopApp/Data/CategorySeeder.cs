using ComputerShopDomainLibrary;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopApp.Data
{
    public class CategorySeeder
    {
        public static async Task Seed(IServiceProvider provider, IWebHostEnvironment environment)
        {
            DbContextOptions<ShopContext> contextOptions = provider.GetRequiredService<DbContextOptions<ShopContext>>();
            using (ShopContext context = new ShopContext(contextOptions))
            {
                if (context.Categories.Any()) return;

                Category category1 = new Category
                {
                    Name = "PC Components"
                };
                Category category2 = new Category
                {
                    Name = "Laptops"
                };

                await context.Categories.AddRangeAsync(category1, category2);
                await context.SaveChangesAsync();
            }
        }
    }
}
