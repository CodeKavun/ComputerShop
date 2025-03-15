using System.ComponentModel.DataAnnotations;

namespace ComputerShopApp.Models.DTO.Admin
{
    public class LoginUserDTO
    {
        [Required]
        [Display(Name = "Login")]
        public string Username { get; set; } = default!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
