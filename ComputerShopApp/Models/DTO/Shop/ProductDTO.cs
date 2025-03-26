using ComputerShopDomainLibrary;
using System.ComponentModel.DataAnnotations;

namespace ComputerShopApp.Models.DTO.Shop
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public double Price { get; set; }
        [Display(Name = "Images")]
        public ICollection<ProductImage>? ProductImages { get; set; } = default!;
        public int BrandId { get; set; }
        [Display(Name = "Brand")]
        public BrandDTO? BrandDTO { get; set; } = default!;
        public int CategoryId { get; set; }
        [Display(Name = "Category")]
        public CategoryDTO? CategoryDTO { get; set; } = default!;
    }
}
