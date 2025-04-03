using ComputerShopDomainLibrary;

namespace ComputerShopApp.Models.ViewModels.Shop
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; } = default!;
        public string? ReturnUrl { get; set; }
    }
}
