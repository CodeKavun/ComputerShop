using ComputerShopApp.Extension;
using ComputerShopDomainLibrary;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShopApp.ViewComponents
{
    public class CartDetailsViewComponent : ViewComponent
    {
        private const string key = "key";
        private readonly IHttpContextAccessor httpContextAccessor;

        public CartDetailsViewComponent(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            IEnumerable<CartItem>? cartItems = null;

            if (httpContextAccessor.HttpContext != null)
            {
                cartItems = httpContextAccessor.HttpContext!.Session.Get<List<CartItem>>(key);
                cartItems ??= [];
            }
            else cartItems = [];

            Cart cart = new Cart(httpContextAccessor, cartItems.ToList());
            return View(cart);
        }
    }
}
