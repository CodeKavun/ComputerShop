using ComputerShopDomainLibrary;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ComputerShopApp.Models.DTO.Admin;
using ComputerShopApp.Models.DTO.Users;
using ComputerShopApp.Models.ViewModels.Users;
using ComputerShopApp.Models.DTO.Roles;
using ComputerShopApp.Models.DTO.Shop;

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
