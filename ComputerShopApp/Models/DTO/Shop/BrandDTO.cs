using System.ComponentModel.DataAnnotations;

namespace ComputerShopApp.Models.DTO.Shop
{
    public class BrandDTO
    {
        public int Id { get; set; }
        [Display(Name = "Brand Name")]
        public string Name { get; set; } = default!;
        public string Country { get; set; } = default!;
    }
}
