using ComputerShopApp.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopDomainLibrary
{
    public class Cart
    {
        private ICollection<CartItem> items = new List<CartItem>();
        private readonly IHttpContextAccessor httpContextAccessor;
        private const string key = "cart";

        public IEnumerable<CartItem> CartItems => items;

        public Cart(IHttpContextAccessor httpContextAccessor, IEnumerable<CartItem> items)
        {
            this.items = items.ToList();
            this.httpContextAccessor = httpContextAccessor;
        }

        public void Add(CartItem item)
        {
            CartItem? cartItem = items.FirstOrDefault(t => t.Product.Id == item.Product.Id);
            if (cartItem == null) items.Add(item);
            else cartItem.Count++;

            httpContextAccessor.HttpContext?.Session.Set(key, CartItems);
        }

        public bool Remove(CartItem item)
        {
            bool result = items.Remove(item);
            httpContextAccessor.HttpContext?.Session.Set(key, CartItems);
            return result;
        }

        public bool Remove(int id)
        {
            CartItem? cartItem = items.FirstOrDefault(c => c.Product.Id == id);
            if (cartItem != null) return Remove(cartItem);
            return false;
        }

        public void Clear()
        {
            items = new List<CartItem>();
            httpContextAccessor.HttpContext?.Session.Set(key, CartItems);
        }

        public double GetTotalPrice() => items.Sum(t => t.TotalPrice);
    }
}
