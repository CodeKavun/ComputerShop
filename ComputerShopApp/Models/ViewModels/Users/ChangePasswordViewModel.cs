using System.ComponentModel.DataAnnotations;

namespace ComputerShopApp.Models.ViewModels.Users
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; } = default!;
        public string? Email { get; set; }
        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = default!;
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = default!;
        [Display(Name = "Confirm New Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; } = default!;
    }
}
