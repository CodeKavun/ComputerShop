using ComputerShopDomainLibrary;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ComputerShopApp.Models.DTO.Shop
{
    public class ProductImageDTO
    {
        public int Id { get; set; }
        [Display(Name = "Image")]
        public byte[] ImageData { get; set; } = default!;
        public int PeoductId { get; set; }
        public ProductDTO? ProductDTO { get; set; } = default!;
    }
}
