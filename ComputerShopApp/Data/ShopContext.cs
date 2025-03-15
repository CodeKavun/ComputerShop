using ComputerShopDomainLibrary;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ComputerShopApp.Models.DTO.Admin;

namespace ComputerShopApp.Data
{
    public class ShopContext : IdentityDbContext<ShopUser>
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }
    }
}
