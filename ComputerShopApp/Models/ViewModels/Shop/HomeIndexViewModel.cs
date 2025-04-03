using ComputerShopDomainLibrary;

namespace ComputerShopApp.Models.ViewModels.Shop
{
    public class HomeIndexViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public IEnumerable<Product>? Products { get; set; }
    }
}
