using ComputerShopDomainLibrary;
using Microsoft.AspNetCore.Mvc;
using ComputerShopApp.Extension;
using ComputerShopApp.Data;
using ComputerShopApp.Models.ViewModels.Shop;
using ComputerShopApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerShopApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopContext context;

        public CartController(ShopContext context)
        {
            this.context = context;
        }

        public IActionResult Index(Cart cart, string? returnUrl)
        {
            CartIndexViewModel viewModel = new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            };
            return View(viewModel);
        }

        public async Task<IActionResult> AddToCart(int? id, Cart cart, string? returnUrl)
        {
            if (id == null) return NotFound();

            Product? product = await context.Products
                .Include(t => t.Brand)
                .Include(t => t.ProductImages)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (product == null) return NotFound();
            cart.Add(new CartItem { Product = product, Count = 1 });

            return RedirectToAction("Index", new { returnUrl });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(Cart cart, int? id, string? returnUrl)
        {
            if (id == null) return NotFound();
            cart.Remove(id.Value);
            return RedirectToAction("Index", new { returnUrl });
        }

        public IActionResult Show()
        {
            string? userName = HttpContext.Session.GetString("CurrentUser");
            return View(model: userName);
        }

        public IActionResult SetUser()
        {
            HttpContext.Session.SetString("CurrentUser", "Dimoni4");
            return View();
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Clear();
            return View();
        }

        //public Cart GetCart()
        //{
        //    string key = "cart";
        //    IEnumerable<CartItem>? cartItems = HttpContext.Session.Get<IEnumerable<CartItem>>(key);

        //    if (cartItems == null) cartItems = new List<CartItem>();

        //    HttpContext.Session.Set(key, cartItems);
        //    Cart cart = new Cart(cartItems);

        //    return cart;
        //}
    }
}
