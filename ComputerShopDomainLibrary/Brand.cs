using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopDomainLibrary
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Country { get; set; } = default!;
        public ICollection<Product> Products { get; set; } = default!;
    }
}
