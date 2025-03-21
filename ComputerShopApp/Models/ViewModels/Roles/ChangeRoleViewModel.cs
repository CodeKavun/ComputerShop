using ComputerShopApp.Models.DTO.Users;
using Microsoft.AspNetCore.Identity;

namespace ComputerShopApp.Models.ViewModels.Roles
{
    public class ChangeRoleViewModel
    {
        public string Id { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public IEnumerable<IdentityRole>? AllRoles { get; set; } = default!;
        public IList<string>? UserRoles { get; set; } = default!;
        public IEnumerable<string> Roles { get; set; } = default!;
    }
}
