using Microsoft.AspNetCore.Identity;

namespace ComputerShopApp.Data
{
    public class ShopUser : IdentityUser
    {
        public DateOnly BirthDate { get; set; }
    }
}
