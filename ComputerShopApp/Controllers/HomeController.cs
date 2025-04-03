using ComputerShopApp.Data;
using ComputerShopApp.Models.ViewModels.Shop;
using ComputerShopDomainLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ComputerShopApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopContext context;

        public HomeController(ShopContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int itemsPerPage = 10;

            IQueryable<Product> products = context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.ProductImages);

            int productCount = products.Count();
            int totalPages = (int)Math.Ceiling((float)productCount / itemsPerPage);

            products = products.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

            HomeIndexViewModel viewModel = new HomeIndexViewModel
            {
                CurrentPage = page,
                TotalPages = totalPages,
                Products = await products.ToListAsync()
            };

            return View(viewModel);
        }
    }
}
