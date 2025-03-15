using System.ComponentModel.DataAnnotations;

namespace ComputerShopApp.Models.DTO.Admin
{
    public class RegisterUserDTO
    {
        [Required]
        [Display(Name = "Login")]
        public string Username { get; set; } = default!;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;

        [Required]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateOnly BirthDate { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [Required]
        [Display(Name = "Confirm your password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords are not matched!")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
