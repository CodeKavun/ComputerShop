using ComputerShopApp.Models.DTO.Shop;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComputerShopApp.Models.ViewModels.Shop
{
    public class CreateCategoryViewModel
    {
        public CategoryDTO CategoryDTO { get; set; } = default!;
        public SelectList? ParentCategories { get; set; }
    }
}
