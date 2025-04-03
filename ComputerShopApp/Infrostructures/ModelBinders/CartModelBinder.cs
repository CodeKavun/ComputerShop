using ComputerShopApp.Extension;
using ComputerShopDomainLibrary;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ComputerShopApp.Infrostructures.ModelBinders
{
    public class CartModelBinder : IModelBinder
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CartModelBinder(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string key = "cart";
            ArgumentNullException.ThrowIfNull(bindingContext);

            List<CartItem>? cartItems = bindingContext.HttpContext.Session.Get<List<CartItem>>(key);

            if (cartItems == null)
            {
                cartItems = new List<CartItem>();
                bindingContext.HttpContext.Session.Set(key, cartItems);
            }

            Cart cart = new Cart(httpContextAccessor, cartItems);
            bindingContext.Result = ModelBindingResult.Success(cart);

            return Task.CompletedTask;
        }
    }
}
