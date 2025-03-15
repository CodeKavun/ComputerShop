using System.ComponentModel.DataAnnotations;

namespace ComputerShopApp.Models.DTO.Users
{
    public class ShopUserDTO
    {
        public string Id { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateOnly BirthDate { get; set; }
    }
}
