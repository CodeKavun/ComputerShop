using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopDomainLibrary
{
    public class CartItem
    {
        public Product Product { get; set; } = default!;
        public int Count { get; set; }
        public double TotalPrice => Product.Price * Count;
    }
}
