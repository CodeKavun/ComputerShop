using ComputerShopApp.Models.DTO.Shop;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ComputerShopApp.Models.ViewModels.Shop
{
    public class CreateImageViewModel
    {
        [Display(Name = "Brands")]
        public SelectList? BrandsList { get; set; }
        public int? SelectedBrandId { get; set; }
        [Display(Name = "Categories")]
        public SelectList? CategoriesList { get; set; }
        public int? SelectedCategoryId { get; set; }
        public SelectList? ProductsList { get; set; }
        [Display(Name = "Product")]
        public int SelectedProductId { get; set; } = default!;
        public IFormFile[] Images { get; set; } = default!;
    }
}
