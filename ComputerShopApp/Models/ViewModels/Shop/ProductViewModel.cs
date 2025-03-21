using ComputerShopApp.Models.DTO.Shop;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComputerShopApp.Models.ViewModels.Shop
{
    public class ProductViewModel
    {
        public ProductDTO ProductDTO { get; set; } = default!;
        public SelectList BrandList { get; set; } = default!;
        public SelectList CategoryList { get; set; } = default!;
    }
}
