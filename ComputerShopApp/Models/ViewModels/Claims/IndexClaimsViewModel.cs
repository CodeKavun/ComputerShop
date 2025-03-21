using System.Security.Claims;

namespace ComputerShopApp.Models.ViewModels.Claims
{
    public class IndexClaimsViewModel
    {
        public IEnumerable<Claim>? Claims { get; set; }
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
