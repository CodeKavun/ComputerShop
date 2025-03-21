using AutoMapper;
using ComputerShopApp.Data;
using ComputerShopApp.Models.DTO.Shop;
using ComputerShopApp.Models.ViewModels.Shop;
using ComputerShopDomainLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShopContext context;
        private readonly IMapper mapper;

        public ProductsController(ShopContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = context.Products.Include(p => p.Brand).Include(m => m.Category);

            IEnumerable<Product> productList = await products.ToListAsync();
            IEnumerable<ProductDTO> productDTOs = mapper.Map<IEnumerable<ProductDTO>>(productList);

            return View(productDTOs);
        }

        public IActionResult Create()
        {
            ProductViewModel viewModel = new ProductViewModel
            {
                BrandList = new SelectList(context.Brands, "Id", "Name"),
                CategoryList = new SelectList(context.Categories, "Id", "Name")
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            return View();
        }
    }
}
